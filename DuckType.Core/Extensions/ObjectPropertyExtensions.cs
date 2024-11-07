using System;

namespace DuckType.Core.Extensions
{
    public static class ObjectPropertyExtensions {

        public static object GetPropertyValue(this object self, string propName)
        {
            if (self == null) throw new ArgumentException("Value cannot be null.", nameof(self));
            if (propName == null) throw new ArgumentException("Value cannot be null.", nameof(propName));

            if (propName.Contains("."))
            {
                var temp = propName.Split(new char[] {'.'}, 2);
                return GetPropertyValue(GetPropertyValue(self, temp[0]), temp[1]);
            }
            var prop = self.GetType().GetProperty(propName);
            return prop != null ? prop.GetValue(self, null) : null;

        }
    }
}