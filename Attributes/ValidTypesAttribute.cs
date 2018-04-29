using System;

namespace Starship.Unity.Attributes {
    public class ValidTypesAttribute : Attribute {
        public ValidTypesAttribute() {
        }

        public ValidTypesAttribute(params Type[] types) {
            Types = types;
        }

        public Type[] Types;
    }
}