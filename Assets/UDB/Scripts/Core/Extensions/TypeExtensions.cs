using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.UDB.Scripts.Core.Extensions
{
    public static class TypeExtensions
    {
        public static string GetCodeForm(this Type type)
        {
            if (!type.IsGenericType) 
                return type.Name;
            if (type.IsNested && type.DeclaringType.IsGenericType) 
                throw new NotImplementedException();

            var txt = type.Name.Substring(0, type.Name.IndexOf('`')) + "<";
            var cnt = 0;
            foreach (Type arg in type.GetGenericArguments())
            {
                if (cnt > 0) txt += ", ";
                txt += GetCodeForm(arg);
                cnt++;
            }
            return txt + ">";
        }
    }
}
