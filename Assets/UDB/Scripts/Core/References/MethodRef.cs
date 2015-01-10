using System;
using System.Reflection;

namespace Assets.UDB.Scripts.Core
{
    public class MethodRef : MemberRef
    {
        private MethodInfo _methodInfo;
  
        public MethodRef(object target, string methodName) : base(target)
        {
            _methodInfo = Target.GetType().GetMethod(methodName, BindingFlags);
            if (_methodInfo == null)
                throw new ArgumentException("Target does not contain method: " + methodName, "methodName");
        }

        public bool             HasParameters()
        {
            return _methodInfo.GetParameters().Length > 0;
        }
        public ParameterInfo[]  GetParameters()
        {
            return _methodInfo.GetParameters();
        }

        public void     Invoke(object[] parameters = null)
        {
            _methodInfo.Invoke(Target, parameters);
        }
        public Delegate CreateDelegate(Type type)
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
    }
}
