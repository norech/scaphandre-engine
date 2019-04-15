using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScaphandreInjector.CodeModification
{
    /// <summary>
    /// This class contains extension methods for the System.Type class
    /// </summary>
    public static class TypeExtensions
    {
        public static DynamicExtender<object> GenerateDynamicExtender(this Type type)
        {
            var extenderType = typeof(DynamicExtender<>);
            var constructedExtenderType = extenderType.MakeGenericType(type);

            return (DynamicExtender<object>)Activator.CreateInstance(constructedExtenderType);
        }
    }
}
