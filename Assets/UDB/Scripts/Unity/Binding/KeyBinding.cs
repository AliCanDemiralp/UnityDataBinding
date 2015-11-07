using UnityEngine.DataBinding;
using UnityEngine;

namespace UnityEngine.DataBinding
{
    public enum KeyModifier
    {
        None,
        Control,
        Alt,
        Shift
    }
    public enum KeyAction
    {
        GetKeyDown,
        GetKeyUp,
        GetKey,
    }
    public enum HandlerMethodType
    {
        Void,
        KeyCode,
        KeyCodeAndModifier
    }

    public class KeyBinding : MonoBehaviour
    {
        public KeyCode              KeyCode;
        public KeyModifier          KeyModifier;
        public KeyAction            KeyAction;
        public CompMethodInfo  MethodInfo;

        private MethodRef           _methodRef;
        private HandlerMethodType   _methodType;

        private bool IsKeyActive
        {
            get
            {
                if (KeyAction == KeyAction.GetKeyDown && Input.GetKeyDown(KeyCode))
                    return true;
                if (KeyAction == KeyAction.GetKeyUp && Input.GetKeyUp(KeyCode))
                    return true;
                return KeyAction == KeyAction.GetKey && Input.GetKey(KeyCode);
            }
        }
        private bool IsModifierActive
        {
            get
            {
                if (KeyModifier == KeyModifier.None)
                    return true;
                if (KeyModifier == KeyModifier.Control && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))
                    return true;
                if (KeyModifier == KeyModifier.Alt && (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)))
                    return true;
                return KeyModifier == KeyModifier.Shift && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
            }
        }
        private void Invoke()
        {
            switch (_methodType)
            {
                case HandlerMethodType.Void:
                    _methodRef.Invoke();
                    break;
                case HandlerMethodType.KeyCode:
                    _methodRef.Invoke(new object[] { KeyCode });
                    break;
                case HandlerMethodType.KeyCodeAndModifier:
                    _methodRef.Invoke(new object[] { KeyCode, KeyModifier });
                    break;
            }          
        }

        private void Start()
        {
            if (MethodInfo == null)
            {
                Debug.Log("Unable to setup key binding! Component member info is null!");
                enabled = false;
                return;
            }

            _methodRef = MethodInfo.Ref;

            if (_methodRef == null)
            {
                Debug.Log("Unable to setup key binding! Component member info does not point to a method!");
                enabled = false;
                return;
            }

            var methodParameters = _methodRef.GetParameters();
            // Handle methods with (Void) parameter signatures.
            if(methodParameters.Length == 0)
                _methodType = HandlerMethodType.Void;
            // Handle methods with (KeyCode) parameter signatures.
            else if (methodParameters.Length == 1 && methodParameters[0].ParameterType == typeof(KeyCode))
                _methodType = HandlerMethodType.KeyCode;
            // Handle methods with (KeyCode, KeyModifier) parameter signatures.
            else if (methodParameters.Length == 2 && methodParameters[0].ParameterType == typeof(KeyCode)
                && methodParameters[1].ParameterType == typeof(KeyModifier))
                _methodType = HandlerMethodType.KeyCodeAndModifier;
            else
            {
                Debug.Log("Unable to setup key binding! Method signature must be (Void) or (KeyCode) or (KeyCode, KeyModifier)!");
                enabled = false;
            }
        }
        private void Update()
        {
            if (IsKeyActive && IsModifierActive)
                Invoke();
        }
    }
}