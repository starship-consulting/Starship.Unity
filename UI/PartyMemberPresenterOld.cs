using System.Linq;
using Assets.Scripts.Actors;
using Assets.Scripts.Core;
using Assets.Scripts.Events;
using UnityEngine.UI;

namespace Assets.Scripts.UI {
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