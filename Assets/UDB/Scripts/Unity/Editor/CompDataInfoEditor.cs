using System.Linq;
using System.Reflection;
using UnityEditor;

namespace UnityEngine.DataBinding.Editor
{
    [CustomPropertyDrawer(typeof(CompDataInfo))]
    public class CompDataInfoEditor : CompMemberInfoEditor 
    {
        protected override MemberInfo[] GetMemberList(Component component)
        {
            var baseMembers = component
                .GetType()
                .GetMembers(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                .Where(m => (m.MemberType == MemberTypes.Field || m.MemberType == MemberTypes.Property))
                .OrderBy(m => m.Name)
                .ToArray();
            return baseMembers;
        }
    }
}
