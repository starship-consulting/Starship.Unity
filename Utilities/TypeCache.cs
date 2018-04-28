using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Assets.Scripts.Utilities {
    public static class TypeCache {

        static TypeCache() {
            Types = new Dictionary<string, Type>();

            AddAssembly(typeof(TypeCache).Assembly);
            AddAssembly(typeof(GameObject).Assembly);
        }

        public static void AddAssembly(Assembly assembly) {
            var duplicates = new List<Type>();

            foreach (var type in assembly.GetTypes()) {
                var name = type.Name.ToLower();

                // Skip dynamic/runtime classes
                if (name.Contains(">")) {
                    continue;
                }

                if (Types.ContainsKey(name)) {
                    var existing = Types[name];
                    duplicates.Add(type);
                    duplicates.Add(existing);
                }
                else {
                    Types.Add(name, type);
                }
            }

            if (duplicates.Any()) {
                //throw new Exception("Duplicate types found: " + string.Join(" ", duplicates.Select(each => each.FullName + Environment.NewLine)));
            }

            Types = Types.OrderBy(each => each.Key).ToDictionary(each => each.Key, each => each.Value);
        }

        public static Type Lookup(string typeName) {
            lock (Types) {
                typeName = typeName.ToLower();

                return Types.ContainsKey(typeName) ? Types[typeName] : null;
            }
        }

        public static IEnumerable<Type> GetTypesOf<T>(bool includeAbstract = true) {
            var types = Types.Values.Where(each => typeof (T).IsAssignableFrom(each));

            if (!includeAbstract) {
                types = types.Where(each => !each.IsAbstract);
            }

            return types;
        }

        private static Dictionary<string, Type> Types { get; set; }
    }
}