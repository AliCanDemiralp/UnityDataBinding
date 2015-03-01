using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.UDB.Scripts.ListView
{
    public class LayoutException : Exception
    {
        public LayoutException()
        {
        }

        public LayoutException(string message)
            : base(message)
        {
        }

        public LayoutException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
