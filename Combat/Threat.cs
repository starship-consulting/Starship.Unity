using System;
using Starship.Unity.Actors;

namespace Starship.Unity.Combat {
    [Serializable]
    public class Threat {
        public Threat() {
        }

        public Threat(Actor target, int value) {
            Target = target;
            Value = value;
        }

        public int Value;

        public Actor Target;
    }
}