using System;

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
    public Func<object, object> FormatMethod;

    // Cached value from last update used for bi-directional updates.
    private object _lastValue;

    public DataBindingExpr  (DataRef source, DataRef target, BindingMode bindingMode = BindingMode.OneWay, Func<object, object> formatMethod = null)
    {
        Setup(source, target, bindingMode, formatMethod);
    }
   
    public void Setup       (DataRef source, DataRef target, BindingMode bindingMode = BindingMode.OneWay, Func<object, object> formatMethod = null)
    {
        Source          = source;
        Target          = target;
        BindingMode     = bindingMode;
        FormatMethod    = formatMethod;
    }
    public void Update      ()
    {
        if (Source == null || Target == null)
            return;

        if (BindingMode == BindingMode.OneWay)
            Target.Value = FormatValue(Source.Value);
        else if (BindingMode == BindingMode.OneWayToSource)
            Source.Value = FormatValue(Target.Value);
        else if (BindingMode == BindingMode.TwoWay)
        {
            var current = FormatValue(Source.Value);
            if (_lastValue == null || !_lastValue.Equals(current))
            {
                _lastValue      = current;
                Target.Value    = current;
            }
            else
            {
                current = FormatValue(Target.Value);
                if (_lastValue.Equals(current))
                    return;

                _lastValue      = current;
                Source.Value    = current;
            }
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

    private object FormatValue(object value)
    {
        return FormatMethod != null ? FormatMethod.Invoke(value) : value;
    }

}
