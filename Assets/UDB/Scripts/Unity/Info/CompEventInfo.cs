using System;
using System.Reflection;
using UnityEngine.DataBinding;

namespace UnityEngine.DataBinding
{
    [Serializable]
    public class CompEventInfo : CompMemberInfo<EventRef>
    {
        public override bool IsValid
        {
            get
            {
                var memberInfo = MemberInfo;
                if (memberInfo == null)
                    return false;
                return memberInfo is EventInfo;
            }
        }
        public override EventRef Ref
        {
            get
            {
                try
                {
                    return new EventRef(Component, MemberName);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
