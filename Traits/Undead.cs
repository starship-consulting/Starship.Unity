using Starship.Unity.Animations;
using Starship.Unity.Audio;
using Starship.Unity.Commands;
using Starship.Unity.Core;
using Starship.Unity.Enumerations;
using Starship.Unity.Events.Combat;
using Starship.Unity.Events.Models;
using Starship.Unity.Extensions;
using Starship.Unity.Interfaces;

namespace Starship.Unity.Traits {
    public class Undead : BaseComponent, CanTakeDamage, IsTrait {

        protected override void Start() {
            AnimationController = GetComponent<IAnimationController>();
        }

        public void TakeDamage(Damage damage) {
            var amount = damage.Amount;

            if (damage.Type != DamageTypes.Generic) {
                //AnimationController.Play(AnimationTypes.Hit);

                if (HitSound != null) {
                    Publish(new PlaySound(HitSound, transform.position));
                }
            }

            if (damage.Type == DamageTypes.Blunt) {
                Durability -= amount;
            }

            if (Durability <= 0) {
                Durability = 0;

                this.AddComponent<Dead>();
            }
        }

        public Sound HitSound;

        public int AnimationStability = 100;

        public int Durability = 100;
        
        private IAnimationController AnimationController { get; set; }
    }
}