using UnityEditor;

namespace UnityEngine.DataBinding.Editor
{
    public class TextEditor : ScriptableWizard
    {
        public delegate void CallbackDelegate(string text);

        public CallbackDelegate Callback;
        public string Expression;

        public static string InfoText;

        public static TextEditor Show(string title, string infoText, string expression, CallbackDelegate callback)
        {
            InfoText = infoText;

            var dialog = DisplayWizard<TextEditor>(title);
            dialog.minSize = new Vector2(480, 320);
            dialog.Callback = callback;
            dialog.Expression = expression;
            dialog.ShowAuxWindow();
            return dialog;
        }

        void OnGUI()
        {
            GUILayout.Label(InfoText, EditorStyles.wordWrappedLabel);

            EditorGUILayout.Separator();

            var wrap = EditorStyles.textField.wordWrap;
            EditorStyles.textField.wordWrap = true;
            Expression = EditorGUILayout.TextArea(Expression, GUILayout.ExpandHeight(true));
            EditorStyles.textField.wordWrap = wrap;

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Cancel", GUILayout.Width(100)))
            {
                Close();
                GUIUtility.ExitGUI();
            }
            if (GUILayout.Button("Save", GUILayout.Width(100)))
            {
                Callback(Expression);
                Close();
                GUIUtility.ExitGUI();
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}