using System;
using System.Reflection;
using Mono.CSharp;

namespace UnityEngine.DataBinding.Extensions
{
    public static class EvaluatorExtensions
    {
        public static void Prepare()
        {
            Evaluator.Init(new string[] { });
            Evaluator.ReferenceAssembly(Assembly.GetExecutingAssembly());
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var assemblyName = assembly.GetName().Name;
                if (!(assemblyName.StartsWith("Mono.CSharp") ||
                      assemblyName.StartsWith("UnityDomainLoad") ||
                      assemblyName.StartsWith("interactive")))
                {
                    try
                    {
                        Evaluator.ReferenceAssembly(assembly);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            Evaluator.Run("using System;");
            Evaluator.Run("using System.Linq;");
            Evaluator.Run("using System.Collections;");
            Evaluator.Run("using System.Collections.Generic;");
            Evaluator.Run("using UnityEngine;");
        }
    }
}
