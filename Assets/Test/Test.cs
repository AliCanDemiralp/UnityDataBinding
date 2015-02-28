using Assets.UDB.Scripts.Unity.Binding;
using UnityEngine;

public class Test : MonoBehaviour
{
    public delegate void StringIntDelegate(string arg1, int arg2);
    public event StringIntDelegate StringIntEvent;

	private void Start ()
    {
        GetComponent<EventTriggeredDataBinding>()   .DataBindingExpr.FormatMethod = FormatMethod;
        GetComponent<PeriodicDataBinding>()         .DataBindingExpr.FormatMethod = FormatMethod2;

        if (StringIntEvent != null)
            StringIntEvent("LOL", 25);
        if (StringIntEvent != null)
            StringIntEvent("LOL", 35);
        if (StringIntEvent != null)
            StringIntEvent("LOL", 45);
	}

    public object FormatMethod(object input)
    {
        return ((int)input + 666);
    }
    public object FormatMethod2(object input)
    {
        return ((int)input + 42);
    }

    public static void StringIntHandler(string arg1, int arg2)
    {
        Debug.Log("StringIntHandler invoked! Values: " + arg1 + " " + arg2);
    }
    public void VoidHandler()
    {
        Debug.Log("VoidHandler invoked!");
    }
    public void KeyCodeHandler(KeyCode keyCode)
    {
        Debug.Log("KeyCodeHandler invoked! Values: " + keyCode);
    }
    public void KeyCodeAndKeyModifierHandler(KeyCode keyCode, KeyModifier keyModifier)
    {
        Debug.Log("KeyCodeAndKeyModifierHandler invoked! Values: " + keyCode + " " + keyModifier);
    }
}
