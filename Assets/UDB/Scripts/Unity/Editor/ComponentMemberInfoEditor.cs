using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace Assets.UDB.Scripts.Unity
{
    [CustomPropertyDrawer(typeof(ComponentMemberInfo))]
    public class ComponentMemberInfoEditor : PropertyDrawer 
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect contentPosition = EditorGUI.PrefixLabel(position, label);

            EditorGUI.indentLevel = 0;

            contentPosition.height = 16;
            EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("Component"), GUIContent.none);
            contentPosition.y += 18;

            Component   componentProperty   = (Component)   property.FindPropertyRelative("Component").objectReferenceValue;
            string      memberNameProperty  = (string)      property.FindPropertyRelative("MemberName").stringValue;
           
            if(componentProperty != null)
            {
                var members = GetMemberList(componentProperty).Select(m => m.Name).ToArray();
                var memberIndex = FindIndex(members, memberNameProperty);
                var selectedIndex = EditorGUI.Popup(contentPosition, memberIndex, members);
                if (selectedIndex >= 0 && selectedIndex < members.Length)
                {
                    var memberName = members[selectedIndex];
                    if (memberName != memberNameProperty)
                    {
                        property.FindPropertyRelative("MemberName").stringValue = memberName;
                    }
                }
            }


            //EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("MemberName"), GUIContent.none);
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 34F;
        }


        private int FindIndex(string[] list, string value)
        {
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] == value)
                    return i;
            }
            return 0;
        }
        private MemberInfo[] GetMemberList(Component component)
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
