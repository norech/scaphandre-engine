using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace ScaphandreInjector.CodeModification
{
    /// <summary>
    /// This class will allow us to extend unextendable classes
    /// </summary>
    /// <typeparam name="T">The type to extend from</typeparam>
    public class DynamicExtender<T>
    {
        TypeBuilder extensionType;

        public DynamicExtender()
        {
            var assembly = AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName("ScaphandreAssembly"), AssemblyBuilderAccess.Run);
            var module = assembly.DefineDynamicModule("Module");
            extensionType = module.DefineType(typeof(T).Name, TypeAttributes.Public, typeof(T));
        }

        public DynamicExtender<T> ExtendWithInterface<K>()
        {
            extensionType.AddInterfaceImplementation(typeof(K));

            foreach (var v in typeof(K).GetProperties())
            {
                AddProperty(v.Name, v.PropertyType, v.GetGetMethod(), v.GetSetMethod());
            }

            return this;
        }

        public DynamicExtender<T> AddProperty(string name, Type type, MethodInfo baseGetter = null, MethodInfo baseSetter = null)
        {
            var field = extensionType.DefineField("_" + name, type, FieldAttributes.Private);
            var property = extensionType.DefineProperty(name, PropertyAttributes.None, type, new Type[0]);
            var getter = extensionType.DefineMethod("get_" + name, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.Virtual, type, new Type[0]);
            var setter = extensionType.DefineMethod("set_" + name, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.Virtual, null, new Type[] { type });

            var getGenerator = getter.GetILGenerator();
            var setGenerator = setter.GetILGenerator();

            getGenerator.Emit(OpCodes.Ldarg_0);
            getGenerator.Emit(OpCodes.Ldfld, field);
            getGenerator.Emit(OpCodes.Ret);

            setGenerator.Emit(OpCodes.Ldarg_0);
            setGenerator.Emit(OpCodes.Ldarg_1);
            setGenerator.Emit(OpCodes.Stfld, field);
            setGenerator.Emit(OpCodes.Ret);

            property.SetGetMethod(getter);
            property.SetSetMethod(setter);

            if(baseGetter != null)
            {
                extensionType.DefineMethodOverride(getter, baseGetter);
            }
            if (baseSetter != null)
            {
                extensionType.DefineMethodOverride(setter, baseSetter);
            }

            return this;
        }

        public Type CreateType()
        {
            return extensionType.CreateType();
        }

        public object Instantiate(bool nonPublic = false)
        {
            return Activator.CreateInstance(CreateType(), nonPublic);
        }
    }
}
