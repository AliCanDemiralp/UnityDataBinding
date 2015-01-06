using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

public class EventRef : MemberRef, IDisposable
{
    private EventInfo   _eventInfo;
    private Delegate    _dynamicDelegate;

    public EventRef(object target, string memberName)       : base(target, memberName)
    {
    }
    public EventRef(object target, MemberInfo memberInfo)   : base(target, memberInfo)
    {
    }
    public EventRef(object target, EventInfo eventInfo)     : base(target)
    {
        if (eventInfo == null)
            throw new ArgumentException("Event cannot be null!", "eventInfo");
        if (!Target.GetType().GetEvents(BindingFlags).Contains(eventInfo))
            throw new ArgumentException("Target does not contain event: " + eventInfo, "eventInfo");

        SetupEvent(eventInfo);
    }

    public delegate void EventRaisedHandler();

    public  event EventRaisedHandler EventRaised
    {
        add
        {
            // Lazy initialization for dynamic method generation.
            if (_dynamicDelegate == null)
                SetupEventRaisedNotification();
            InternalEventRaised += value;
        }
        remove { InternalEventRaised -= value; }
    }
    private event EventRaisedHandler InternalEventRaised;

    public void Dispose                     ()
    {
        CleanupEventRaisedNotification();
    }
    public bool IsParametersCompatible      (MethodRef methodRef)
    {
        var invokeMethod = _eventInfo.EventHandlerType.GetMethod("Invoke");
        return invokeMethod.IsParametersCompatible(methodRef.GetParameters());
    }

    public Delegate AddHandler              (MethodRef methodRef)
    {
        var handlerDelegate = methodRef.CreateDelegate(_eventInfo.EventHandlerType); 
        _eventInfo.AddEventHandler(Target, handlerDelegate);
        return handlerDelegate;
    }
    public void     RemoveHandler           (Delegate methodDelegate)
    {
        _eventInfo.RemoveEventHandler(Target, methodDelegate);   
    }

    public override string ToString()
    {
        var objectString    = Target        != null ? Target.ToString() : "[NULL]";
        var eventString     = _eventInfo     != null ? _eventInfo.Name   : "[NULL]";
        return objectString + "." + eventString;
    }

    protected override void SetupMember(MemberInfo memberInfo)
    {
        if (memberInfo is EventInfo)
            SetupEvent((EventInfo)memberInfo);
        else
            throw new ArgumentException("Member " + memberInfo + "is not an event", "memberInfo");
    }

    private void SetupEvent(EventInfo eventInfo)
    {
        _eventInfo = eventInfo;
    }
    private void SetupEventRaisedNotification()
    {
        var eventSignature = _eventInfo.EventHandlerType;

        if (eventSignature.BaseType != typeof (MulticastDelegate))
            throw new ArgumentException("Type is not multicast delegate: " + eventSignature, "type");

        var invokeMethodInfo = eventSignature.GetMethod("Invoke");
        if (invokeMethodInfo == null)
            throw new InvalidOperationException("Unable to get Invoke method of type: " + eventSignature);
        if (invokeMethodInfo.ReturnType != typeof (void))
            throw new ApplicationException("Delegate has a return type.");

        // Generate a method which will be called when event is triggered.
        var types = new List<Type> {GetType()};
        types.AddRange(invokeMethodInfo.GetParameterTypes());
        var generatedMethod = new DynamicMethod(string.Empty, typeof (void), types.ToArray(), GetType());

        // Fill in the method with a call to the local OnDynamicHandlerInvoked() and return.
        var ilgen = generatedMethod.GetILGenerator();
        ilgen.Emit(OpCodes.Ldarg_0);
        ilgen.Emit(OpCodes.Call,
            GetType().GetMethod("OnDynamicHandlerInvoked", BindingFlags.Instance | BindingFlags.NonPublic));
        ilgen.Emit(OpCodes.Ret);

        // Create and store the delegate to the generated method.
        _dynamicDelegate = generatedMethod.CreateDelegate(eventSignature, this);

        // Register the generated method to event.
        _eventInfo.AddEventHandler(Target, _dynamicDelegate);
    }
    private void CleanupEventRaisedNotification()
    {
        if (_dynamicDelegate != null)
            _eventInfo.RemoveEventHandler(Target, _dynamicDelegate);
    }
    private void OnDynamicHandlerInvoked()
    {
        if (InternalEventRaised != null)
            InternalEventRaised();
    }
}