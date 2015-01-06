using System;
using System.Linq;
using System.Reflection;

using UnityEngine;

[Serializable]
public class ComponentMemberInfo
{
    public Component    Component;
    public string       MemberName;

    public bool         IsValid
    {
        get
        {
            return Component != null && !string.IsNullOrEmpty(MemberName) && MemberType != null;
        }
    }
    public Type         MemberType
    {
        get
        {
            var memberInfo = Component.GetType().GetMember(MemberName).FirstOrDefault();
            if (memberInfo == null)
                return null;

            if (memberInfo is FieldInfo)
                return ((FieldInfo)memberInfo).FieldType;
            if (memberInfo is PropertyInfo)
                return ((PropertyInfo)memberInfo).PropertyType;
            if (memberInfo is MethodInfo)
                return ((MethodInfo)memberInfo).ReturnType;
            if (memberInfo is EventInfo)
                return ((EventInfo)memberInfo).EventHandlerType;

            return null;         
        }
    }

    public DataRef      AsDataRef   ()
    {
        return new DataRef(Component, MemberName);
    }
    public MethodRef    AsMethodRef ()
    {
        return new MethodRef(Component, MemberName);
    }
    public EventRef     AsEventRef  ()
    {
        return new EventRef(Component, MemberName);
    }

    public override string ToString()
    {
        var componentString = Component != null ? Component.GetType().Name  : "[NULL]";
        var memberString    = IsValid           ? MemberName                : "[NULL]";

        return componentString + "." + memberString;
    }
}
