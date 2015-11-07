using UnityEditor;

namespace UnityEngine.UI.Extensions.Editor
{
    [CustomEditor(typeof(ListView))]
    public class ListViewEditor : UnityEditor.Editor 
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            var script = serializedObject.FindProperty("m_Script");
            EditorGUILayout.PropertyField(script, true, new GUILayoutOption[0]);
            serializedObject.ApplyModifiedProperties();

            var listView = (ListView) target;
            
            listView.ListItem = (GameObject) EditorGUILayout.ObjectField("List Item", listView.ListItem, typeof(GameObject), false);

            if (GUILayout.Button("Edit Adapter Function", EditorStyles.popup))
                DataBinding.Editor.TextEditor.Show(
                    "List Adapter Function Editor",
                    "Please enter the format below.\nYou can use 'data' and 'view' as references.\nExample: view.GetComponent<Text>.text = data.Name;",
                    listView.AdapterString, (text) => { ((ListView)target).AdapterString = text; });
        }
    }
}
