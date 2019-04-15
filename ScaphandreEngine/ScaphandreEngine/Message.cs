using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScaphandreEngine
{
    public static class Message
    {
        public static void Error(string message)
        {
            ErrorMessage.AddError(message);
        }
    }
}
