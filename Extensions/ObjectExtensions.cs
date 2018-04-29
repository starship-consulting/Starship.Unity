using System;
using System.Reflection;

namespace Starship.Unity.Extensions {
    public static class ObjectExtensions {

        public static T ReadField<T>(this object source, string fieldName) {
            return (T)source.GetType().FindField(fieldName).GetValue(source);
        }

        public static T ReadProperty<T>(this object source, string propertyName) {
            return (T)source.GetType().FindProperty(propertyName).GetValue(source, null);
        }

        public static T ReadMember<T>(this object source, string memberName) {
            var member = source.GetType().FindMember(memberName);

            if (member == null) {
                return default(T);
            }

            switch (member.MemberType) {
                case MemberTypes.Field:
                    return (T)member.As<FieldInfo>().GetValue(source);
                case MemberTypes.Property:
                    return (T)member.As<PropertyInfo>().GetValue(source, null);
            }

            return default(T);
        }

        public static void CopyTo(this object from, object to) {
            var type = from.GetType();
            var properties = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
            var fields = type.GetFields();

            foreach (var property in properties) {
                if (property.GetSetMethod() == null) {
                    continue;
                }

                var value = property.GetValue(from, new object[] {});
                property.SetValue(to, value, new object[] {});
            }

            foreach (var field in fields) {
                var value = field.GetValue(from);
                field.SetValue(to, value);
            }
        }

        public static T As<T>(this object instance) where T : class {
            
            if (instance == null) {
                return default(T);
            }

            var cast = instance as T;

            if (cast != null) {
                return cast;
            }

            return (T)Convert.ChangeType(instance, typeof (T));
        }
    }
}
