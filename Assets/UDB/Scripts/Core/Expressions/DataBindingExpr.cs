using System;
using Mono.CSharp;
using UnityEngine.DataBinding.Extensions;

namespace UnityEngine.DataBinding
{
    public enum BindingMode
    {
        OneWay,
        OneWayToSource,
        TwoWay
    }

    [Serializable]
    public class DataBindingExpr
    {
        public DataRef              Source;
        public DataRef              Target;
        public BindingMode          BindingMode;

        [SerializeField]
        public Func<object, object> FormatMethod;

        [SerializeField]
        private string              _formatString;
        public  string              FormatString
        {
            get { return _formatString; }
            set
            {
                _formatString = value;
                Debug.Log("Format string updated!");
                UpdateFormatMethodFromString();
            }
        }

        // Cached value from last update used for bi-directional updates.
        private object _lastValue;

        public DataBindingExpr( DataRef                 source, 
                                DataRef                 target, 
                                BindingMode             bindingMode     = BindingMode.OneWay,
                                Func<object, object>    formatMethod    = null)
        {
            Source          = source;
            Target          = target;
            BindingMode     = bindingMode;
            FormatMethod    = formatMethod;
        }

        public void Update()
        {
            if (!string.IsNullOrEmpty(FormatString) && FormatMethod == null)
                UpdateFormatMethodFromString();

            if (Source == null || Target == null)
                return;

            try
            {
                if (BindingMode == BindingMode.OneWay)
                    Target.Value = FormatMethod == null ? Source.Value : FormatMethod.Invoke(Source.Value);
                else if (BindingMode == BindingMode.OneWayToSource)
                    Source.Value = FormatMethod == null ? Target.Value : FormatMethod.Invoke(Target.Value);
                else if (BindingMode == BindingMode.TwoWay)
                {
                    var current = FormatMethod == null ? Source.Value : FormatMethod.Invoke(Source.Value);
                    if (_lastValue == null || !_lastValue.Equals(current))
                    {
                        _lastValue = current;
                        Target.Value = current;
                    }
                    else
                    {
                        current = FormatMethod == null ? Target.Value : FormatMethod.Invoke(Target.Value);
                        if (_lastValue.Equals(current))
                            return;

                        _lastValue = current;
                        Source.Value = current;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log("Conversion error on " + ToString() + " : " + e.Message);
            }
            
        }

        public override string ToString()
        {
            var sourceString = Source != null ? Source.ToString() : "[NULL]";
            var targetString = Target != null ? Target.ToString() : "[NULL]";

            string bindingString;
            switch (BindingMode)
            {
                case BindingMode.OneWay:
                    bindingString = " -> ";
                    break;
                case BindingMode.OneWayToSource:
                    bindingString = " <- ";
                    break;
                default:
                    bindingString = " <> ";
                    break;
            }

            return "Data Binding Expression: " + sourceString + bindingString + targetString;
        }

        private void UpdateFormatMethodFromString()
        {
            if (Source == null)
            {
                Debug.Log("Format method not set. Source type is unknown at compile-time.");
                return;
            }

            EvaluatorExtensions.Prepare();
            try
            {
                FormatMethod = (Func<object, object>)Evaluator.Evaluate("new Func<object, object>((src) => { " +
                    Source.Type.GetCodeForm() + " source = (" + Source.Type.GetCodeForm() + ") src;" + FormatString + " });");
            }
            catch (Exception)
            {
                Debug.Log("Syntax error on format string of " + ToString() + " : " + FormatString);
                return;
            }
            Debug.Log("Format method set from string: " + FormatString);
        }
    }
}