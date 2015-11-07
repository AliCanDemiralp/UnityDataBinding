namespace UnityEngine.DataBinding
{
    public class EventBindingComponent : MonoBehaviour
    {
        public bool BindAtStart = true;

        public CompEventInfo EventInfo;
        public CompMethodInfo HandlerInfo;

        private EventBinding _coreEventBinding;

        public void Bind()
        {
            if (_coreEventBinding != null && !_coreEventBinding.IsBound)
                _coreEventBinding.Bind();
        }

        public void Unbind()
        {
            if (_coreEventBinding != null && _coreEventBinding.IsBound)
                _coreEventBinding.Unbind();
        }

        private void Start()
        {
            if (_coreEventBinding == null)
                _coreEventBinding = new EventBinding();

            if (!_coreEventBinding.Setup(EventInfo.Ref, HandlerInfo.Ref))
            {
                Debug.Log("Event binding setup failure! Event / handler parameters not compatible!");
                return;
            }

            if (BindAtStart)
                Bind();
        }

        private void OnDestroy()
        {
            Unbind();

            if (_coreEventBinding != null)
                _coreEventBinding.Dispose();
        }
    }
}