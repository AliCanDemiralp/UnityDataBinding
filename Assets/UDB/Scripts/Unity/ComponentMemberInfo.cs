using Assets.UDB.Scripts.Core;
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Assets.UDB.Scripts.Unity
{
    [Serializable]
    public class ComponentMemberInfo
    {
        public Component    Component;
        public string       MemberName;

        public bool         IsValid
        {
            get
            {
                if (Component == null)
                    return false;

                if (string.IsNullOrEmpty(MemberName))
                    return false;

                var memberInfo = Component.GetType().GetMember(MemberName).FirstOrDefault();
                if (memberInfo == null)
                    return false;

                return  memberInfo is FieldInfo     || 
                        memberInfo is PropertyInfo  || 
                        memberInfo is MethodInfo    ||
                        memberInfo is EventInfo;
            }
        }

        public DataRef      AsDataRef   ()
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
        public MethodRef    AsMethodRef ()
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
        public EventRef     AsEventRef  ()
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

        public override string ToString()
        {
            var componentString = Component != null ? Component.GetType().Name  : "[NULL]";
            var memberString    = IsValid           ? MemberName                : "[NULL]";

            return componentString + "." + memberString;
        }
    }
}
