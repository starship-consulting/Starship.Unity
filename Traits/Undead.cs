using Assets.Scripts.Animations;
using Assets.Scripts.Audio;
using Assets.Scripts.Commands;
using Assets.Scripts.Core;
using Assets.Scripts.Enumerations;
using Assets.Scripts.Events.Combat;
using Assets.Scripts.Events.Models;
using Assets.Scripts.Extensions;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.Traits {
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