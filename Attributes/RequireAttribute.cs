using System;

namespace Starship.Unity.Attributes {

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class RequireAttribute : Attribute {
        public RequireAttribute(Type behaviorType) {
            Type = behaviorType;
        }

        public Type Type { get; set; }
    }
}