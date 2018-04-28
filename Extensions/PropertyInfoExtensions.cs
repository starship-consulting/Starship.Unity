using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Assets.Scripts.Extensions {
    public static class PropertyInfoExtensions {
        public static bool HasAttribute<T>(this PropertyInfo property) where T : Attribute {
            return property.GetAttributes<T>().Any();
        }

        public static IEnumerable<T> GetAttributes<T>(this PropertyInfo property, bool inherit = true) where T : Attribute {
            return property.GetCustomAttributes(typeof(T), inherit).OfType<T>();
        }

        public static bool Is<T>(this PropertyInfo property) {
            return property.PropertyType.IsAssignableFrom(typeof(T));
        }
    }
}