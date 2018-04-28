using System;
using Assets.Scripts.Actors;
using Assets.Scripts.Components;
using Assets.Scripts.Models;

namespace Assets.Scripts.Events {
    [Serializable]
    public struct ActorStateStarted {
        public Actor Actor;

        public ActorState State;
    }
}