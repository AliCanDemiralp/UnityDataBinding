using System;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public ComponentMemberInfo  SourceInfo;
    public ComponentMemberInfo  TargetInfo;
    public DataBindingExpr      DataBindingExpr;

    public delegate void TestHandler(string arg1, int arg2);
    public event TestHandler TestOccured;

    public string prefixString = "PREFIX!!! ";
    public Weapon CurrentWeapon = new Weapon() {Ammo = 33, Desc = "LOL WEP DESC", Name = "LOL WEP"};

    public List<Weapon> weapons = new List<Weapon>(); 

	private void Start ()
	{
        weapons.Add(new Weapon() { Ammo = 12, Desc = "Simple gun.", Name = "Pistol" });
        weapons.Add(new Weapon() { Ammo = 30, Desc = "Machine gun.", Name = "MP5" });
        weapons.Add(new Weapon() { Ammo = 50, Desc = "Ultra machine gun.", Name = "AK74" });

        GetComponent<PeriodicDataBinding>().DataBindingExpr.FormatMethod = FormatMethod;

	    //DataBindingExpr.Source = SourceInfo.AsDataRef();
	    //DataBindingExpr.Target = TargetInfo.AsDataRef();
        //DataBindingExpr.FormatMethod = o => ((int) o + 42).ToString();
        //DataBindingExpr.Update();
        //Debug.Log(DataBindingExpr);

        //var eventRef = new EventRef(this, "TestOccured");
	    //eventRef.EventRaised += OnEventRaised;
        //TestOccured("LOL", 15);
        //TestOccured("LOL", 25);
        //eventRef.Dispose();
        //if(TestOccured != null)
        //    TestOccured("LOL", 25);

        //var e1 = new EventBinding(new EventRef(this, "TestOccured"), new MethodRef(this, "NullHandler"));
        //var e2 = new EventBinding(new EventRef(this, "TestOccured"), new MethodRef(this, "MatchHandler"));
        //Debug.Log(e1);
        //Debug.Log(e2);

        if(TestOccured != null)
            TestOccured("LOL", 25);

        //e1.Unbind();

        if (TestOccured != null)
            TestOccured("LOL", 35);

        //e2.Unbind();

        if (TestOccured != null)
            TestOccured("LOL", 45);
	}

    private object FormatMethod(object o)
    {
        var weps = (List<Weapon>) o;

        return null;
        //return new List<WeaponUI>
    }

    private void OnEventRaised(EventRef eventRef)
    {
        Debug.Log("Event raised: " + eventRef);
    }

    public static void MatchHandler(string arg1, int arg2)
    {
        Debug.Log("Match handler invoked! Values: " + arg1 + " " + arg2);
    }
    public void NullHandler()
    {
        Debug.Log("Null handler invoked!");
    }
    public void KeyHandler(KeyCode keyCode)
    {
        Debug.Log("Key handler invoked: " + keyCode);
    }
    public void KeyAndModifierHandler(KeyCode keyCode, KeyModifier keyModifier)
    {
        Debug.Log("Key & modifier handler invoked: " + keyCode + " " + keyModifier);
    }
}
