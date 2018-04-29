using System.Collections.Generic;
using Starship.Unity.Combat;
using Starship.Unity.Core;
using Starship.Unity.Events;
using Starship.Unity.Events.Targetting;

namespace Starship.Unity.Players {
    
    public class Party : BaseComponent {
        
        protected override void Awake() {
            base.Awake();
            //On<TargetSelected>(OnTargetSelected);
            //On<CharacterJoinedParty>(OnCharacterJoinedParty);
        }

        public IEnumerable<PartyMember> GetMembers() {
            return Members;
        }

        protected override void Start() {
            base.Start();

            //foreach (var member in Members) {
            //    Publish(new CharacterJoinedParty(member));
            //}
        }

        private void OnCharacterJoinedParty(CharacterJoinedParty e) {
            //var members = Members.ToList();
            //members.Add(e.Character);
            //Members = members.ToArray();
        }

        private void OnTargetSelected(TargetSelected e) {
            if (e.Target == null) {
                Attack(null);
                return;
            }

            var attackable = e.Target.GetComponent<Attackable>();

            if (attackable == null) {
                return;
            }

            Attack(attackable);
        }

        private void Attack(Attackable target) {
            //foreach (var member in Members) {
            //    member.Attack(target);
            //}
        }

        public List<PartyMember> Members;
    }
}