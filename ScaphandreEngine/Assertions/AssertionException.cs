using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScaphandreEngine.Assertions
{
    class AssertionException : Exception
    {
        public AssertionException(string msg) : base(msg)
        {
            //
        }
    }
}
