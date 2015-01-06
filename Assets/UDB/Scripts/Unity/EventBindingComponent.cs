using UnityEngine;

public class EventBindingComponent : MonoBehaviour
{
    public bool BindAtStart = true;

    public ComponentMemberInfo  EventInfo;
    public ComponentMemberInfo  HandlerInfo;
    public EventBinding         EventBinding;

    public  void Bind()
    {
        if (EventInfo == null)
        {
            Debug.Log("Event binding unsuccessful. Event info is null!");
            return;
        }
        if (HandlerInfo == null)
        {
            Debug.Log("Event binding unsuccessful. Handler info is null!");
            return;
        }

        EventBinding.Bind(EventInfo.AsEventRef(), HandlerInfo.AsMethodRef());
    }

    private void Start      ()
    {
        if (BindAtStart)
            Bind();
    }
    private void OnDestroy  ()
    {
        if(EventBinding.IsBound)
            EventBinding.Unbind();
    }

}
