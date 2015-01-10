using System;
using System.Linq;
using System.Reflection;

public static class MethodInfoExtensions 
{
    public static Type[] GetParameterTypes(this MethodInfo methodInfo)
    {
        if (methodInfo == null)
            return new Type[0];

        var parameterInfos = methodInfo.GetParameters();
        var parameterTypes = new Type[parameterInfos.Length];
        for (var i = 0; i < parameterInfos.Length; i++)
            parameterTypes[i] = parameterInfos[i].ParameterType;
        return parameterTypes;
    }

    public static bool IsParametersCompatible(this MethodInfo methodInfo, ParameterInfo[] parameters)
    {
        if (methodInfo == null || parameters == null)
            return false;

        var parameterInfos = methodInfo.GetParameters();
        if (parameterInfos.Length != parameters.Length)
            return false;

        return !parameterInfos.Where((t, i) => !t.IsCompatible(parameters[i])).Any();
    }
    public static bool IsParametersCompatible(this MethodInfo methodInfo, MethodInfo other)
    {
        if (methodInfo == null || other == null)
            return false;

        return IsParametersCompatible(methodInfo, other.GetParameters());
    }
}
