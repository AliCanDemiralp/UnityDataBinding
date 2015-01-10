using UnityEngine;

namespace Assets.UDB.Scripts.Unity
{
    public class EventBinding : MonoBehaviour
    {
        public bool BindAtStart = true;

        public ComponentMemberInfo  EventInfo;
        public ComponentMemberInfo  HandlerInfo;
        
        private Core.EventBinding   _coreEventBinding;

        public void Bind    ()
        {
            if (_coreEventBinding != null && !_coreEventBinding.IsBound)
                _coreEventBinding.Bind();
        }
        public void Unbind  ()
        {
            if (_coreEventBinding != null && _coreEventBinding.IsBound)
                _coreEventBinding.Unbind();
        }

        private void Start      ()
        {
            if(_coreEventBinding == null)
                _coreEventBinding = new Core.EventBinding();

            if (!_coreEventBinding.Setup(EventInfo.AsEventRef(), HandlerInfo.AsMethodRef()))
            {
                Debug.Log("Event binding setup failure! Event / handler parameters not compatible!");
                return;
            }
           
            if (BindAtStart)
                Bind();
        }
        private void OnDestroy  ()
        {
            Unbind();

            if (_coreEventBinding != null)
                _coreEventBinding.Dispose();
        }
    }
}
