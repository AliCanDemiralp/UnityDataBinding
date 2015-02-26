using System;
using System.Reflection;
using Assets.UDB.Scripts.Core;

namespace Assets.UDB.Scripts.Unity.Info
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
