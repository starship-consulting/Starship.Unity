using System;
using System.Reflection;
using Assets.Scripts.Utilities;

namespace Assets.Scripts.Models {

    [Serializable]
    public class TypeBinding {

        public Type GetBindingType() {
            if (string.IsNullOrEmpty(TypeName)) {
                return null;
            }

            return TypeCache.Lookup(TypeName);
        }

        public FieldInfo GetBindingField() {
            var type = GetBindingType();
            return type == null ? null : type.GetField(FieldName);
        }

        public string TypeName;

        public string FieldName;
    }
}