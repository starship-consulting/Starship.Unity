using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Assets.Scripts.Core {

    [Serializable]
    public class SerializableObjectField {

        public MonoBehaviour Source;

        public string PropertyName;

        public void SetValue(object value) {
            var member = GetMember();

            if (member != null) {
                var type = Source.GetType();

                switch (member.MemberType) {
                    case MemberTypes.Property:
                        var property = type.GetProperty(member.Name);
                        
                        if (value != null && property.PropertyType != value.GetType()) {
                            value = Convert.ChangeType(value, property.PropertyType);
                        }

                        property.SetValue(Source, value, null);
                        break;
                    case MemberTypes.Field:

                        var field = type.GetField(member.Name);

                        if (value != null && field.FieldType != value.GetType()) {
                            value = Convert.ChangeType(value, field.FieldType);
                        }

                        field.SetValue(Source, value);
                        break;
                }
            }
        }

        public MemberInfo GetMember() {
            return Source != null ? Source.GetType().GetMember(PropertyName).FirstOrDefault() : null;
        }
    }
}