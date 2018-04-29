using Starship.Unity.Actors;
using Starship.Unity.Core;
using Starship.Unity.Enumerations;
using Starship.Unity.Events;
using Starship.Unity.Events.Combat;
using Starship.Unity.Events.Models;
using Starship.Unity.Movement;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Starship.Unity.Combat {
    public class Attackable : BaseComponent, IPointerClickHandler {

        protected override void Start() {
            base.Start();
            //FloatingText = GetComponentInChildren<FloatingText>();
        }

        public void OnPointerClick(PointerEventData e) {
            /*if (Player.Instance == null) {
                return;
            }*/

            return;

            //var player = Player.Instance.GetOrAdd<Attacker>();

            Add<MoveToObject>(move => {
                //move.Speed = Speed;
                //move.Destination = position;
                //move.Range = range;
            });

            //Attack(playerActor);
        }

        public void Attack(Actor source = null) {
            if (AttackEffect != null) {
                Instantiate(AttackEffect, new Vector3(transform.position.x, 2, transform.position.z - 1), Quaternion.identity);
            }

            Hit(new Damage(20, DamageTypes.Blunt, source));
        }

        public void Hit(Damage damage) {
            Publish<IsDamageListener>(e => e.OnDamage(damage));
            //FloatingText.Display(damage.Amount, Color.red, CombatTextType.Hit);

            if (damage.Amount > 0) {
                Publish<CanAvoidDamage>(e => e.AvoidDamage(damage));
            }

            if (damage.Amount > 0) {
                Publish<CanMitigateDamage>(e => e.MitigateDamage(damage));
            }

            if (damage.Amount > 0) {
                Publish<CanTakeDamage>(e => e.TakeDamage(damage));
            }
        }
        
        public GameObject AttackEffect;

        //private FloatingText FloatingText { get; set; }
    }
}