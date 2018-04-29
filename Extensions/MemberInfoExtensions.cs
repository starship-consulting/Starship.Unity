using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Starship.Unity.Extensions {
    public static class MemberInfoExtensions {
        public static bool HasAttribute<T>(this MemberInfo member) where T : Attribute {
            return member.GetAttributes<T>().Any();
        }

        public static IEnumerable<T> GetAttributes<T>(this MemberInfo member, bool inherit = true) where T : Attribute {
            return member.GetCustomAttributes(typeof(T), inherit).OfType<T>();
        }

        public static bool IsPublic(this MemberInfo member) {
            var property = member as PropertyInfo;

            if (property != null) {
                return property.CanWrite && property.GetSetMethod(false) != null;
            }

            var field = member as FieldInfo;

            if (field != null && field.IsPublic) {
                return true;
            }

            var e = member as EventInfo;

            if (e != null && e.IsPublic()) {
                return true;
            }

            return false;
        }

        public static Type GetMemberType(this MemberInfo member) {
            switch (member.MemberType) {
                case MemberTypes.Field:
                    return member.As<FieldInfo>().FieldType;
                case MemberTypes.Property:
                    return member.As<PropertyInfo>().PropertyType;
            }

            return null;
        }

        public static object Get(this MemberInfo member, object source) {
            switch (member.MemberType) {
                case MemberTypes.Field:
                    member.As<FieldInfo>().GetValue(source);
                    break;
                case MemberTypes.Property:
                    member.As<PropertyInfo>().GetValue(source, null);
                    break;
            }

            return null;
        }

        public static void Set(this MemberInfo member, object source, object value) {
            switch (member.MemberType) {
                case MemberTypes.Field:
                    member.As<FieldInfo>().SetValue(source, value);
                    break;
                case MemberTypes.Property:
                    member.As<PropertyInfo>().SetValue(source, value, null);
                    break;
            }
        }
    }
}