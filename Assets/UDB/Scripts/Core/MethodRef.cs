using System;
using System.Linq;
using System.Reflection;

public class MethodRef : MemberRef
{
    private MethodInfo _methodInfo;
  
    public MethodRef(object target, string      memberName) : base(target, memberName)
    {
    }
    public MethodRef(object target, MemberInfo  memberInfo) : base(target, memberInfo)
    {
    }
    public MethodRef(object target, MethodInfo  methodInfo) : base(target)
    {
        if (methodInfo == null)
            throw new ArgumentException("Method cannot be null!", "methodInfo");
        if (!Target.GetType().GetMethods(BindingFlags).Contains(methodInfo))
            throw new ArgumentException("Target does not contain method: " + methodInfo, "methodInfo");

        SetupMethod(methodInfo);
    }

    public bool             HasParameters   ()
    {
        return _methodInfo.GetParameters().Length > 0;
    }
    public ParameterInfo[]  GetParameters   ()
    {
        return _methodInfo.GetParameters();
    }
    public void             Invoke          (object[] parameters = null)
    {
        _methodInfo.Invoke(Target, parameters);
    }
    public Delegate         CreateDelegate  (Type type)
    {
#if !UNITY_EDITOR && UNITY_METRO
        if(_methodInfo.IsStatic)
            return _methodInfo.CreateDelegate(type, null);
        else
            return _methodInfo.CreateDelegate(type, Target);
#else
        if (_methodInfo.IsStatic)
            return Delegate.CreateDelegate(type, null, _methodInfo, true);
        else
            return Delegate.CreateDelegate(type, Target, _methodInfo, true);
#endif
    }

    public override string ToString()
    {
        var objectString = Target      != null ? Target.ToString() : "[NULL]";
        var methodString = _methodInfo != null ? _methodInfo.Name  : "[NULL]";

        return objectString + "." + methodString;
    }

    protected override void SetupMember(MemberInfo memberInfo)
    {
        if (memberInfo is MethodInfo)
            SetupMethod((MethodInfo)memberInfo);
        else
            throw new ArgumentException("Member " + memberInfo + "is not a method", "memberInfo");
    }

    private void SetupMethod(MethodInfo methodInfo)
    {
        _methodInfo = methodInfo;
    }
}
