using UnityEngine;

public enum KeyAction
{
    GetKeyDown,
    GetKeyUp,
    GetKey,
}

public enum KeyModifier
{
    None,
    Control,
    Alt,
    Shift
}

public enum HandlerMethodType
{
    Void,
    KeyCode,
    KeyCodeAndModifier
}

public class KeyBinding : MonoBehaviour
{
    public KeyCode      KeyCode;
    public KeyModifier  KeyModifier;
    public KeyAction    KeyAction;

    public ComponentMemberInfo MethodInfo;

    private MethodRef           _methodRef;
    private HandlerMethodType   _methodType;

    private bool IsKeyDown
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
    private bool IsModifierDown
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

    private void Start()
    {
        if (MethodInfo == null)
        {
            Debug.Log("Unable to setup key binding! Method info is null!");
            return;
        }
        _methodRef = MethodInfo.AsMethodRef();

        var methodParameters = _methodRef.GetParameters();
        // Handle methods with (KeyCode) parameter signatures.
        if(methodParameters.Length == 1 && methodParameters[0].ParameterType == typeof(KeyCode))
            _methodType = HandlerMethodType.KeyCode;
        // Handle methods with (KeyCode, KeyModifier) parameter signatures.
        if (methodParameters.Length == 2 && methodParameters[0].ParameterType == typeof(KeyCode)
                                         && methodParameters[1].ParameterType == typeof(KeyModifier))
            _methodType = HandlerMethodType.KeyCodeAndModifier;
        
    }
    private void Update()
    {
        if (IsKeyDown && IsModifierDown)
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
    }
}
