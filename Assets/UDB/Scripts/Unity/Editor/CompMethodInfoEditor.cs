using System.Linq;
using System.Reflection;
using Assets.UDB.Scripts.Unity.Info;
using UnityEditor;
using UnityEngine;

namespace Assets.UDB.Scripts.Unity.Editor
{
    [CustomPropertyDrawer(typeof(CompMethodInfo))]
    public class CompMethodInfoEditor : CompMemberInfoEditor
    {
        protected override MemberInfo[] GetMemberList(Component component)
        {
            var baseMembers = component
                .GetType()
                .GetMembers(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                .Where(m => (m.MemberType == MemberTypes.Method))
                .OrderBy(m => m.Name)
                .ToArray();
            return baseMembers;
        }
    }
}
