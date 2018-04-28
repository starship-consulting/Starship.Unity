using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Assets.Scripts.Extensions {
    public static class TypeExtensions {

        public static object GetDefaultValue(this Type type) {
            var method = typeof(TypeExtensions).GetMethod("GetDefaultValueInternal", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly);
            return method.MakeGenericMethod(type).Invoke(null, null);
        }

        private static object GetDefaultValueInternal<T>() {
            return default(T);
        }

        public static FieldInfo FindField(this Type type, string name) {
            return type.GetField(name, BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public static PropertyInfo FindProperty(this Type type, string name) {
            return type.GetProperty(name, BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        }
        
        public static MemberInfo FindMember(this Type type, string name) {
            return type.GetMember(name, BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault();
        }

        public static bool HasAttribute<T>(this Type type) where T : Attribute {
            return type.GetAttributes<T>().Any();
        }

        public static IEnumerable<T> GetAttributes<T>(this Type type, bool inherit = true) where T : Attribute {
            return type.GetCustomAttributes(typeof(T), inherit).OfType<T>();
        }

        public static bool IsSimple(this Type type) {
            if (!type.IsEnum && !type.FullName.StartsWith("System.")) {
                return false;
            }

            if (type.IsGenericType && type.GetEnumerableType() != null) {
                return false;
            }

            return true;
        }

        public static List<MethodInfo> GetRealMethods(this Type type, bool publicOnly = false) {
            var flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

            if (publicOnly) {
                flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;
            }

            return type.GetMethods(flags).Where(m => !m.IsSpecialName).ToList();
        }
        
        public static IEnumerable<Type> GetGenericInterfacesOfType<T>(this Type type) {
            return type.GetInterfacesOfType<T>().Where(each => each.IsGenericType);
        }

        public static IEnumerable<Type> GetInterfacesOfType<T>(this Type type) {
            return type.GetBaseTypes().Where(each => each.IsInterface && typeof(T).IsAssignableFrom(each));
        }

        public static List<Type> GetBaseTypes(this Type type) {
            var types = type.GetInterfaces().ToList();

            if (type.BaseType != null && !types.Contains(type.BaseType)) {
                types.Add(type.BaseType);
            }

            foreach (var eachBaseType in types.ToList()) {
                var parentTypes = eachBaseType.GetBaseTypes();

                foreach (var parentType in parentTypes) {
                    if (!types.Contains(parentType)) {
                        types.Add(parentType);
                    }
                }
            }

            return types;
        }

        public static object New(this Type type) {
            return Activator.CreateInstance(type);
        }

        public static T New<T>(this Type type, params Type[] genericTypes) {
            if (genericTypes.Any()) {
                type = type.MakeGenericType(genericTypes);
            }

            return (T) Activator.CreateInstance(type);
        }

        public static Type GetEnumerableType(this Type type) {
            return (from intType in type.GetInterfaces()
                where intType.IsGenericType && intType.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                select intType.GetGenericArguments()[0]).FirstOrDefault();
        }

        public static IList MakeList(this Type type) {
            Type genericListType = typeof(List<>).MakeGenericType(type);
            return (IList) Activator.CreateInstance(genericListType);
        }

        public static bool Is<T>(this Type type) {
            return typeof(T).IsAssignableFrom(type);
        }

        public static bool IsCollection(this Type type) {
            return type.Is<IList>() || type.Is<ICollection>() || type.Is<IEnumerable>();
        }

        public static bool IsNullableType(this Type type) {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static bool IsNonSystemClass(this Type type) {
            return type.Namespace != "System" && !type.IsPrimitive;
        }

        public static List<Type> GetTypesOf(this Type type, bool includeAbstract = false) {
            return Assembly.GetAssembly(type).GetTypes()
                .Where(each => type.IsAssignableFrom(each) && (includeAbstract || each.IsAbstract == false))
                .ToList();
        }

        public static T ReadPrivateField<T>(this object instance, string fieldName) {
            var field = instance.GetPrivateField(fieldName);
            return (T)field.GetValue(instance);
        }

        public static FieldInfo GetPrivateField(this Type type, string fieldName) {
            var field = type.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);

            if(field == null && type.BaseType != null && type.BaseType != typeof(object)) {
                return type.BaseType.GetPrivateField(fieldName);
            }

            return field;
        }

        public static FieldInfo GetPrivateField(this object instance, string fieldName) {
            return instance.GetType().GetPrivateField(fieldName);
        }
    }
}