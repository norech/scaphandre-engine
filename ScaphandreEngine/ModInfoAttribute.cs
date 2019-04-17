using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScaphandreEngine
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ModInfoAttribute : Attribute
    {
        public string id;
        public string name;
        public string version;
        public string author;
    }
}
