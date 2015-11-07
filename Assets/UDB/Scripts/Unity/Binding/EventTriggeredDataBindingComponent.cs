using UnityEngine.DataBinding;
using UnityEngine;

namespace UnityEngine.DataBinding
{
    public class EventTriggeredDataBindingComponent : MonoBehaviour
    {
        public bool BindAtStart     = true;
        public bool UpdateAtStart   = true;

        public CompEventInfo   TriggerEventInfo;
        public CompDataInfo    SourceInfo;
        public CompDataInfo    TargetInfo;

        public DataBindingExpr      DataBindingExpr;

        private UnityEngine.DataBinding.EventTriggeredDataBinding _coreDataBinding;

        public void Bind()
        {
            if (_coreDataBinding != null && !_coreDataBinding.IsBound)
                _coreDataBinding.Bind();
        }
        public void Unbind()
        {
            if (_coreDataBinding != null && _coreDataBinding.IsBound)
                _coreDataBinding.Unbind();
        }

        private void Start          ()
        {
            DataBindingExpr.Source = SourceInfo.Ref;
            DataBindingExpr.Target = TargetInfo.Ref;

            if (_coreDataBinding == null)
                _coreDataBinding = new UnityEngine.DataBinding.EventTriggeredDataBinding();
            if (!_coreDataBinding.Setup(DataBindingExpr, TriggerEventInfo.Ref))
            {
                Debug.Log("Event-triggered data binding setup failure!");
                enabled = false;
                return;
            }

            if (UpdateAtStart)
                DataBindingExpr.Update();
            if (BindAtStart)
                Bind();
        }
        private void OnDestroy      ()
        {
            Unbind();

            if (_coreDataBinding != null)
                _coreDataBinding.Dispose();
        }
    }
}
