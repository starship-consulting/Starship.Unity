using Assets.Scripts.Core;
using Assets.Scripts.Events;
using Assets.Scripts.Extensions;
using Assets.Scripts.Players;

namespace Assets.Scripts.UI {
    //[ExecuteInEditMode]
    public class PartyPresenter : BaseComponent {
        //protected void Awake() {
        //this.ClearChildren(true);

        //if (Party != null) {
        //    On<CharacterJoinedParty>(OnCharacterJoinedParty);
        //}
        //}

        protected override void OnEnable() {
            base.OnEnable();
            UpdateState();
        }
        
        public void UpdateState() {

            this.ClearChildren();

            if (Template == null) {
                return;
            }

            foreach (var member in Party.Members) {
                var instance = this.Create(Template);
                instance.Member = member;
                instance.UpdateState();
            }
        }

        private void OnCharacterJoinedParty(CharacterJoinedParty e) {
            //var view = this.Create(Template);
            //view.SetCharacter(e.Character);
        }

        public Party Party;

        public PartyMemberPresenter Template;
    }
}