using System;
using System.Reflection;

namespace Assets.UDB.Scripts.Core
{
    public abstract class MemberRef 
    {
        protected const BindingFlags BindingFlags = System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Static;

        protected object Target { get; private set; }

        protected MemberRef(object target)
        {
            Target = target;
            if (Target == null)
                throw new ArgumentException("Target object cannot be null", "target");          
        }
    }
}
