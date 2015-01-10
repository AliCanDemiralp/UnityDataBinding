using System;

namespace Assets.UDB.Scripts.Core
{
    public class EventTriggeredDataBinding : IDisposable
    {
        private DataBindingExpr _dataBindingExpr;
        private EventRef        _triggerEventRef;

        public bool IsBound { get; private set; }

        public bool Setup   (DataBindingExpr dataBindingExpr, EventRef triggerEventRef)
        {
            Dispose();

            _dataBindingExpr = dataBindingExpr;
            _triggerEventRef = triggerEventRef;

            return _dataBindingExpr != null && _triggerEventRef != null;
        }
        public void Dispose ()
        {
            if(IsBound)
                Unbind();

            if (_triggerEventRef != null)
                _triggerEventRef.Dispose();
        }

        public bool Bind    ()
        {
            if (IsBound || _dataBindingExpr == null || _triggerEventRef == null)
                return false;

            _triggerEventRef.EventRaised += OnTriggerEventRaised;

            IsBound = true;

            return true;
        }
        public void Unbind  ()
        {
            if (!IsBound)
                return;

            if (_triggerEventRef != null)
                _triggerEventRef.EventRaised -= OnTriggerEventRaised;

            IsBound = false;
        }
        
        public override string ToString()
        {
            var expressionString    = _dataBindingExpr != null ? _dataBindingExpr.ToString() : "[NULL]";
            var triggerString       = _triggerEventRef != null ? _triggerEventRef.ToString() : "[NULL]";
            return "Event Triggered Data Binding: " + expressionString + " triggered by " + triggerString;
        }

        private void OnTriggerEventRaised()
        {
            _dataBindingExpr.Update();
        }
    }
}
