using Assets.UDB.Scripts.Core;
using UnityEngine;
using UnityEditor;

namespace Assets.UDB.Scripts.Unity
{
    [CustomPropertyDrawer(typeof (DataBindingExpr))]
    public class DataBindingExprEditor : PropertyDrawer
    {
        private string _formatString;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var contentPosition = EditorGUI.PrefixLabel(position, label);
            
            EditorGUI.indentLevel = 0;
            
            contentPosition.height = 16;

            EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("BindingMode"), GUIContent.none);

            // contentPosition.y += 18;

            // EditorGUI.TextArea(contentPosition, _formatString);
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 16F;
            // return 34F;
        }
    }
}