using System;
using Assets.UDB.Scripts.Core;
using UnityEditor;
using UnityEngine;

namespace Assets.UDB.Scripts.Unity.Editor
{
    [CustomPropertyDrawer(typeof(DataBindingExpr))]
    public class DataBindingExprEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var contentPosition = EditorGUI.PrefixLabel(position, label);
            
            EditorGUI.indentLevel = 0;
            
            contentPosition.height = 16;

            EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("BindingMode"), GUIContent.none);

            contentPosition.y += 18;

            var dbe = (DataBindingExpr) fieldInfo.GetValue(property.serializedObject.targetObject);

            if (GUI.Button(contentPosition, "Edit Format Function", EditorStyles.popup))
                TextEditor.Show(
                    "Format Function Editor", 
                    "Please enter the format below.\nYou can use 'source' to refer to data binding expression.\nAn example from int source to string target would be: return \"Value: \" + source;",
                    dbe.FormatString, (text) => { dbe.FormatString = text; });
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 34F;
        }
    }
}