using System;
using Assets.Scripts.Enumerations;
using Assets.Scripts.ScriptableObjects;

namespace Assets.Scripts.Controls {

    [Serializable]
    public class MouseBinding {

        public MouseButtons Button;

        public ButtonStates State;

        public GameEvent Event;

        public override string ToString() {
            return Button + " " + State + " -> " + Event.name;
        }
    }
}