using UnityEngine;

public class EventTriggeredDataBinding : MonoBehaviour 
{
    public bool UpdateAtStart = true;

    public ComponentMemberInfo  TriggerEventInfo;
    public ComponentMemberInfo  SourceInfo;
    public ComponentMemberInfo  TargetInfo;
    public DataBindingExpr      DataBindingExpr;

    private EventRef            _triggerEventRef;

    private void Start          ()
    {
        DataBindingExpr.Source = SourceInfo.AsDataRef();
        DataBindingExpr.Target = TargetInfo.AsDataRef();

        _triggerEventRef = TriggerEventInfo.AsEventRef();
        _triggerEventRef.EventRaised += OnEventRaised;

        if(UpdateAtStart)
            DataBindingExpr.Update();
    }
    private void OnDestroy      ()
    {
        if(_triggerEventRef != null)
            _triggerEventRef.Dispose();
    }
    private void OnEventRaised  ()
    {
        DataBindingExpr.Update();
    }
}
