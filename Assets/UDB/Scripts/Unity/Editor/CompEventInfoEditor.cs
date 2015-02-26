using System.Linq;
using System.Reflection;
using Assets.UDB.Scripts.Unity;
using Assets.UDB.Scripts.Unity.Info;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(CompEventInfo))]
public class CompEventInfoEditor : CompMemberInfoEditor 
{
    protected override MemberInfo[] GetMemberList(Component component)
    {
        var baseMembers = component
            .GetType()
            .GetMembers(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
            .Where(m => (m.MemberType == MemberTypes.Event))
            .OrderBy(m => m.Name)
            .ToArray();
        return baseMembers;
    }
}
