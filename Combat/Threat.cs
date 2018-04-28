using System;
using Assets.Scripts.Actors;

namespace Assets.Scripts.Combat {
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