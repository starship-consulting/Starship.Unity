using System;
using Assets.Scripts.Actors;
using Assets.Scripts.Components;

namespace Assets.Scripts.Events {
    public class CharacterJoinedParty {

        public CharacterJoinedParty() {
        }

        public CharacterJoinedParty(Actor character) {
            Character = character;
        }

        public Actor Character { get; set; }
    }
}