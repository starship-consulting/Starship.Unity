using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Assets.Scripts.Extensions {
    public static class AssemblyExtensions {

        public static IEnumerable<Type> GetTypesOf<T>(this Assembly assembly, bool includeAbstract = true) {
            var types = assembly.GetTypes().Where(each => (includeAbstract || !each.IsAbstract) && typeof(T).IsAssignableFrom(each));
            return types;
        }
    }
}
