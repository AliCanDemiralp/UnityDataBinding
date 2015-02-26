using System;
using System.Reflection;
using Assets.UDB.Scripts.Core;

namespace Assets.UDB.Scripts.Unity.Info
{
    [Serializable]
    public class CompDataInfo : CompMemberInfo<DataRef>
    {
        public override bool IsValid
        {
            get
            {
                var memberInfo = MemberInfo;
                if (memberInfo == null)
                    return false;
                return memberInfo is FieldInfo ||
                       memberInfo is PropertyInfo;
            }
        }
        public override DataRef Ref
        {
            get
            {
                try
                {
                    return new DataRef(Component, MemberName);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
