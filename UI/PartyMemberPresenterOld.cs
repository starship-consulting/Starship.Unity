using Starship.Unity.Actors;
using Starship.Unity.Core;
using UnityEngine.UI;

namespace Starship.Unity.UI {
    public class PartyMemberPresenterOld : BaseComponent {

        public void SetCharacter(Actor character) {
            Character = character;
            Portrait.sprite = character.Portrait;

            //On<CharacterPropertyChanged>(Character, OnPropertyChanged);

            //Resists.SetProperties(character.GetResists().ToArray());
            //Stats.SetProperties(character.GetStats().ToArray());
        }

        /*private void OnPropertyChanged(CharacterPropertyChanged e) {

            if (e.Property.IsResist) {
                Resists.SetProperties(e.Property);
            }
            else if (e.Property.IsStat) {
            }
        }*/

        public Image Portrait;

        public Actor Character;

        public PropertiesPresenter Stats;

        public PropertiesPresenter Resists;
    }
}