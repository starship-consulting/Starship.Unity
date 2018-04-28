using System;

namespace Assets.Scripts.Attributes {

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class RequireAttribute : Attribute {
        public RequireAttribute(Type behaviorType) {
            Type = behaviorType;
        }

        public Type Type { get; set; }
    }
}