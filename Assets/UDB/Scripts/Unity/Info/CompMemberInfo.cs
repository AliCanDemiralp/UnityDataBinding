using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Assets.UDB.Scripts.Unity.Info
{
    [Serializable]
    public abstract class CompMemberInfo<T>
    {
        public Component    Component;
        public string       MemberName;

        public MemberInfo   MemberInfo
        {
            get
            {
                if (Component == null)
                    return null;

                if (string.IsNullOrEmpty(MemberName))
                    return null;

                return Component.GetType().GetMember(MemberName).FirstOrDefault();
            }              
        }

        public abstract bool    IsValid { get; }
        public abstract T       Ref     { get; }

        public override string ToString()
        {
            var componentString = Component != null                 ? Component.GetType().Name  : "[NULL]";
            var memberString    = !string.IsNullOrEmpty(MemberName) ? MemberName                : "[NULL]";
            return componentString + "." + memberString;
        }
    }
}
