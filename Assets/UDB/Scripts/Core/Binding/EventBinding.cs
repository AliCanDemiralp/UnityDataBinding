using System;

namespace Assets.UDB.Scripts.Core
{
    public class EventBinding : IDisposable
    {
        private EventRef    _event;
        private MethodRef   _handler;

        private Delegate    _handlerDelegate;

        public bool IsBound { get; private set; }

        public bool Setup(EventRef sourceEvent, MethodRef targetHandler)
        {
            Dispose();

            _event      = sourceEvent;
            _handler    = targetHandler;

            if (_event == null || _handler == null)
                return false;

            return _event.IsParametersCompatible(_handler) || !_handler.HasParameters();
        }
        public void Dispose()
        {
            if (IsBound)
                Unbind();

            if (_event != null)
                _event.Dispose();
        }

        public bool Bind    ()
        {
            if (IsBound || _event == null || _handler == null)
                return false;

            // If handler is compatible with event, register normally.
            if (_event.IsParametersCompatible(_handler))
                _handlerDelegate = _event.AddHandler(_handler);
            // Otherwise, register to notification of event and trigger handler manually.
            else if (!_handler.HasParameters())
                _event.EventRaised += OnEventRaisedNotification;
            else
                return false;

            IsBound = true;

            return true;
        }
        public void Unbind  ()
        {
            if (!IsBound)
                return;

            _event.EventRaised -= OnEventRaisedNotification;

            if(_handlerDelegate != null)
                _event.RemoveHandler(_handlerDelegate);

            IsBound = false;
        }

        public override string ToString()
        {
            var eventString     = _event    != null ? _event.ToString()     : "[NULL]";
            var handlerString   = _handler  != null ? _handler.ToString()   : "[NULL]";
            return "Event Binding: " + handlerString + " handles " + eventString;
        }

        private void OnEventRaisedNotification()
        {
            _handler.Invoke();
        }
    }
}
