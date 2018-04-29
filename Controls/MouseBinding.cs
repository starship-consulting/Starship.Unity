using System;
using Starship.Unity.Enumerations;
using Starship.Unity.ScriptableObjects;

namespace Starship.Unity.Controls {

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