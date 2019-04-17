using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScaphandreEngine.Events
{
    public class ListenEventAttribute : Attribute
    {
        public EventPriority priority;
    }
}
