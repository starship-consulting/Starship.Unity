using Starship.Unity.Actors;

namespace Starship.Unity.Events {
    public class CharacterJoinedParty {

        public CharacterJoinedParty() {
        }

        public CharacterJoinedParty(Actor character) {
            Character = character;
        }

        public Actor Character { get; set; }
    }
}