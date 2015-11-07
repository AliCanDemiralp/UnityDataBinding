using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace UnityEngine.DataBinding
{
    public class EventRef : MemberRef, IDisposable
    {
        public delegate void EventRaisedHandler();

        private readonly EventInfo _eventInfo;
        private Delegate _dynamicDelegate;

        public EventRef(object target, string eventName) : base(target)
        {
            _eventInfo = Target.GetType().GetEvent(eventName, BindingFlags);
            if (_eventInfo == null)
                throw new ArgumentException("Target does not contain event: " + eventName, "eventName");
        }

        public void Dispose()
        {
            if (_dynamicDelegate != null)
                _eventInfo.RemoveEventHandler(Target, _dynamicDelegate);
        }

        public event EventRaisedHandler EventRaised
        {
            add
            {
                // Lazy initialization for dynamic method generation.
                if (_dynamicDelegate == null)
                {
                    Type eventSignature = _eventInfo.EventHandlerType;

                    if (eventSignature.BaseType != typeof (MulticastDelegate))
                        throw new ArgumentException("Type is not multicast delegate: " + eventSignature, "type");

                    MethodInfo invokeMethodInfo = eventSignature.GetMethod("Invoke");
                    if (invokeMethodInfo == null)
                        throw new InvalidOperationException("Unable to get Invoke method of type: " + eventSignature);
                    if (invokeMethodInfo.ReturnType != typeof (void))
                        throw new ApplicationException("Delegate has a return type.");

                    // Generate a method which will be called when event is triggered.
                    var types = new List<Type> {GetType()};
                    types.AddRange(invokeMethodInfo.GetParameterTypes());
                    var generatedMethod = new DynamicMethod(string.Empty, typeof (void), types.ToArray(), GetType());

                    // Fill in the method with a call to the local OnDynamicHandlerInvoked() and return.
                    ILGenerator ilgen = generatedMethod.GetILGenerator();
                    ilgen.Emit(OpCodes.Ldarg_0);
                    ilgen.Emit(OpCodes.Call,
                        GetType().GetMethod("OnDynamicHandlerInvoked", BindingFlags.Instance | BindingFlags.NonPublic));
                    ilgen.Emit(OpCodes.Ret);

                    // Create and store the delegate to the generated method.
                    _dynamicDelegate = generatedMethod.CreateDelegate(eventSignature, this);

                    // Register the generated method to event.
                    _eventInfo.AddEventHandler(Target, _dynamicDelegate);
                }
                InternalEventRaised += value;
            }
            remove { InternalEventRaised -= value; }
        }

        private event EventRaisedHandler InternalEventRaised;

        public bool IsParametersCompatible(MethodRef methodRef)
        {
            if (methodRef == null)
                return false;

            MethodInfo invokeMethod = _eventInfo.EventHandlerType.GetMethod("Invoke");
            return invokeMethod.IsParametersCompatible(methodRef.GetParameters());
        }

        public Delegate AddHandler(MethodRef methodRef)
        {
            if (methodRef == null || !IsParametersCompatible(methodRef))
                return null;

            Delegate handlerDelegate = methodRef.CreateDelegate(_eventInfo.EventHandlerType);
            _eventInfo.AddEventHandler(Target, handlerDelegate);
            return handlerDelegate;
        }

        public void RemoveHandler(Delegate methodDelegate)
        {
            if (methodDelegate == null)
                return;

            _eventInfo.RemoveEventHandler(Target, methodDelegate);
        }

        public override string ToString()
        {
            string objectString = Target != null ? Target.ToString() : "[NULL]";
            string eventString = _eventInfo != null ? _eventInfo.Name : "[NULL]";
            return objectString + "." + eventString;
        }

        private void OnDynamicHandlerInvoked()
        {
            if (InternalEventRaised != null)
                InternalEventRaised();
        }
    }
}