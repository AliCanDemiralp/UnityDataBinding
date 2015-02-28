using Assets.UDB.Scripts.Core;
using Assets.UDB.Scripts.Unity.Info;
using UnityEngine;

namespace Assets.UDB.Scripts.Unity.Binding
{
    public class EventTriggeredDataBinding : MonoBehaviour
    {
        public bool BindAtStart     = true;
        public bool UpdateAtStart   = true;

        public CompEventInfo   TriggerEventInfo;
        public CompDataInfo    SourceInfo;
        public CompDataInfo    TargetInfo;

        public DataBindingExpr      DataBindingExpr;

        private Core.EventTriggeredDataBinding _coreDataBinding;

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
                _coreDataBinding = new Core.EventTriggeredDataBinding();
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
