using System.Reflection;

public static class ParameterInfoExtensions
{
    public static bool IsCompatible(this ParameterInfo parameterInfo, ParameterInfo other)
    {
        return parameterInfo.ParameterType.Equals           (other.ParameterType) ||
               parameterInfo.ParameterType.IsAssignableFrom (other.ParameterType);
    }
}
