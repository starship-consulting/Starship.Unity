using System;
using Starship.Unity.Actors;
using Starship.Unity.Models;

namespace Starship.Unity.Events {

    [Serializable]
    public struct ActorStateEnded {
        public Actor Actor;

        public ActorState State;
    }
}