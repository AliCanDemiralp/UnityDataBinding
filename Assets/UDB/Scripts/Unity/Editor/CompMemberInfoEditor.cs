using System.Linq;
using System.Reflection;
using UnityEditor;

namespace UnityEngine.DataBinding.Editor
{
    public abstract class CompMemberInfoEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Component componentProperty = (Component) property.FindPropertyRelative("Component").objectReferenceValue;
            string memberNameProperty = (string) property.FindPropertyRelative("MemberName").stringValue;

            Rect contentPosition = EditorGUI.PrefixLabel(position, label);
            EditorGUI.indentLevel = 0;
            contentPosition.height = 16;

            EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("Component"), GUIContent.none);

            if (componentProperty != null)
            {
                contentPosition.y += 18;

                var components = componentProperty.gameObject.GetComponents(typeof (Component));
                var componentStrings = components.Select(m => m.GetType().ToString()).ToArray();
                var componentIndex = componentStrings.ToList().IndexOf(componentProperty.GetType().ToString());
                var selectedComponentIndex = EditorGUI.Popup(contentPosition, componentIndex, componentStrings);
                if (selectedComponentIndex >= 0 && selectedComponentIndex < components.Length)
                {
                    var component = components[selectedComponentIndex];
                    if (component != componentProperty)
                    {
                        property.FindPropertyRelative("Component").objectReferenceValue = component;
                        return;
                    }
                }
                contentPosition.y += 18;

                var members = GetMemberList(componentProperty).Select(m => m.Name).ToArray();
                var memberIndex = members.ToList().IndexOf(memberNameProperty);
                var selectedMemberIndex = EditorGUI.Popup(contentPosition, memberIndex, members);
                if (selectedMemberIndex >= 0 && selectedMemberIndex < members.Length)
                {
                    var memberName = members[selectedMemberIndex];
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
            return 52F;
        }

        protected abstract MemberInfo[] GetMemberList(Component component);
    }
}
