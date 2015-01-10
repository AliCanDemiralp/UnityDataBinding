using System.Reflection;

public static class ParameterInfoExtensions
{
    public static bool IsCompatible(this ParameterInfo parameterInfo, ParameterInfo other)
    {
        if (parameterInfo == null || other == null)
            return false;

        return parameterInfo.ParameterType == other.ParameterType ||
               parameterInfo.ParameterType.IsAssignableFrom (other.ParameterType);
    }
}
