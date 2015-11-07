using System;
using System.Reflection;
using UnityEngine.DataBinding;

namespace UnityEngine.DataBinding
{
    [Serializable]
    public class CompMethodInfo : CompMemberInfo<MethodRef>
    {
        public override bool IsValid
        {
            get
            {
                var memberInfo = MemberInfo;
                if (memberInfo == null)
                    return false;
                return memberInfo is MethodInfo;
            }
        }
        public override MethodRef Ref
        {
            get
            {
                try
                {
                    return new MethodRef(Component, MemberName);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
