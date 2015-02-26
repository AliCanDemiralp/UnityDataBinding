using Assets.UDB.Scripts.Core;
using Assets.UDB.Scripts.Unity.Info;
using UnityEngine;

namespace Assets.UDB.Scripts.Unity
{
    public enum PeriodicMethod
    {
        OnUpdate,
        OnLateUpdate,
        OnFixedUpdate
    }

    [ExecuteInEditMode]
    public class PeriodicDataBinding : MonoBehaviour
    {
        public bool UpdateInEditMode    = false;
        public bool UpdateAtStart       = true;

        public CompDataInfo    SourceInfo;
        public CompDataInfo    TargetInfo;
        public DataBindingExpr      DataBindingExpr;
        public PeriodicMethod       PeriodicMethod;

        private void Start          ()
        {
            if (Application.isPlaying)
            {
                DataBindingExpr.Source = SourceInfo.Ref;
                DataBindingExpr.Target = TargetInfo.Ref;       
            }
        
            if (CanUpdate() && UpdateAtStart)
                DataBindingExpr.Update();
        }
        private void Update         ()
        {
            if (CanUpdate() && PeriodicMethod == PeriodicMethod.OnUpdate)
                DataBindingExpr.Update();
        }
        private void LateUpdate     ()
        {
            if (CanUpdate() && PeriodicMethod == PeriodicMethod.OnLateUpdate)
                DataBindingExpr.Update();
        }
        private void FixedUpdate    ()
        {
            if (CanUpdate() && PeriodicMethod == PeriodicMethod.OnFixedUpdate)
                DataBindingExpr.Update();
        }

        private bool CanUpdate      ()
        {
#if UNITY_EDITOR
            if (!UpdateInEditMode && !Application.isPlaying)
                return false;
#endif
            return true;
        }
    }
}