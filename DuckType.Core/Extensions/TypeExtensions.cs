using System;
using System.Linq;
using System.Reflection;

namespace DuckType.Core.Extensions
{
    public static class TypeExtensions
    {
        public static Type GetNoneSealedType(this object self)
        {
            var t = self.GetType();
            return GetFirstNoneSealedType(t);
        }

        private static Type GetFirstNoneSealedType(Type t)
        {
            return !t.IsSealed ? t : GetFirstNoneSealedType(t.BaseType);
        }
        
        public static object GetDefault(this Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
        
        public static bool IsImplementationOf(this MethodInfo self, MethodInfo other)
        {
            if (self.DeclaringType != null && !self.DeclaringType.IsAssignableFrom(other.DeclaringType)) return false;
            if (self.Name != other.Name) return false;
            if (other.GetParameters().Length != self.GetParameters().Length) return false;
            var parameterTypes = self.GetParameters().Select(p => p.ParameterType);
            var setParameterTypes = other.GetParameters().Select(p => p.ParameterType);
            return parameterTypes.SequenceEqual(setParameterTypes);
        }
    }
}
