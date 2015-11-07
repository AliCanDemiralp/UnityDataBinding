using System.Linq;
using System.Reflection;
using UnityEditor;

namespace UnityEngine.DataBinding.Editor
{
    [CustomPropertyDrawer(typeof(CompEventInfo))]
    public class CompEventInfoEditor : CompMemberInfoEditor 
    {
        protected override MemberInfo[] GetMemberList(Component component)
        {
            var baseMembers = component
                .GetType()
                .GetMembers(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                .Where(m => ((m.MemberType == MemberTypes.Event) /*|| (m.MemberType == MemberTypes.Property && ((PropertyInfo)m).PropertyType.BaseType == typeof(UnityEvent))*/))
                .OrderBy(m => m.Name)
                .ToArray();
            return baseMembers;
        }
    }
}
