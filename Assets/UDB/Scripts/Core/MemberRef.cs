using System;
using System.Linq;
using System.Reflection;

public abstract class MemberRef 
{
    protected const BindingFlags BindingFlags = System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Static;

    private     object _target;

    public      object Target
    {
        get { return _target; }
        private set
        {
            if (value == null)
                throw new ArgumentException("Target object cannot be null!", "target");

            _target = value;
        }
    }

    protected MemberRef(object target)
    {
        Target = target;
    }
    protected MemberRef(object target, string       memberName) : this(target)
    {
        var memberInfo = _target.GetType().GetMember(memberName, BindingFlags).FirstOrDefault();
        if (memberInfo == null)
            throw new ArgumentException("Target does not contain member with name: " + memberName, "memberName");

        SetupMember(memberInfo);
    }
    protected MemberRef(object target, MemberInfo   memberInfo) : this(target)
    {
        if (memberInfo == null)
            throw new ArgumentException("Member cannot be null!", "memberInfo");
        if (!_target.GetType().GetMembers(BindingFlags).Contains(memberInfo))
            throw new ArgumentException("Target does not contain member: " + memberInfo, "memberInfo");

        SetupMember(memberInfo);
    }

    protected abstract void SetupMember(MemberInfo memberInfo);
}
