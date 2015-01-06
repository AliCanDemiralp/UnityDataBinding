using System;

[Serializable]
public class EventBinding
{
    public  EventRef    Event;
    public  MethodRef   Handler;

    private Delegate    _handlerDelegate;

    public bool IsBound { get; private set; }

    public EventBinding (EventRef sourceEvent, MethodRef targetHandler)
    {
        Bind(sourceEvent, targetHandler);
    }

    public void Bind    (EventRef sourceEvent, MethodRef targetHandler)
    {
        // Clean previous setup first if any.
        if(IsBound)
            Unbind();

        Event   = sourceEvent;
        Handler = targetHandler;

        if(Event == null || Handler == null)
            throw new NullReferenceException("Source event or target handler cannot be null!");

        // If handler is compatible with event, register normally.
        if (Event.IsParametersCompatible(Handler))
            _handlerDelegate = Event.AddHandler(Handler);
        // Otherwise, register to notification of event and trigger handler manually.
        else if (!Handler.HasParameters())
            Event.EventRaised += OnEventRaisedNotification;
        else
            throw new ArgumentException("Handler is not compatible with event!", "targetHandler");

        IsBound = true;
    }
    public void Unbind  ()
    {
        if (!IsBound)
            return;

        Event.EventRaised -= OnEventRaisedNotification;

        if(_handlerDelegate != null)
            Event.RemoveHandler(_handlerDelegate);

        if(Event != null)
            Event.Dispose();

        IsBound = false;
    }

    public override string ToString()
    {
        var eventString     = Event     != null ? Event.ToString()      : "[NULL]";
        var handlerString   = Handler   != null ? Handler.ToString()    : "[NULL]";
        return "Event Binding: " + handlerString + " handles " + eventString;
    }

    private void OnEventRaisedNotification()
    {
        Handler.Invoke();
    }
}
