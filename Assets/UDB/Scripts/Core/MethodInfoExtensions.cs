using System;
using System.Linq;
using System.Reflection;

public static class MethodInfoExtensions 
{
    public static Type[] GetParameterTypes(this MethodInfo methodInfo)
    {
        var parameterInfos = methodInfo.GetParameters();
        var parameterTypes = new Type[parameterInfos.Length];
        for (var i = 0; i < parameterInfos.Length; i++)
            parameterTypes[i] = parameterInfos[i].ParameterType;
        return parameterTypes;
    }

    public static bool IsParametersCompatible(this MethodInfo methodInfo, ParameterInfo[] parameters)
    {
        var parameterInfos = methodInfo.GetParameters();
        if (parameterInfos.Length != parameters.Length)
            return false;

        return !parameterInfos.Where((t, i) => !t.IsCompatible(parameters[i])).Any();
    }
    public static bool IsParametersCompatible(this MethodInfo methodInfo, MethodInfo other)
    {
        return IsParametersCompatible(methodInfo, other.GetParameters());
    }
}
