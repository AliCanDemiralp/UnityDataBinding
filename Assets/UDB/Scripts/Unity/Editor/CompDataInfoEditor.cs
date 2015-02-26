using System.Linq;
using System.Reflection;
using Assets.UDB.Scripts.Unity;
using Assets.UDB.Scripts.Unity.Info;
using UnityEditor;
using UnityEngine;

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
