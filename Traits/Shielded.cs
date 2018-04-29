using System;
using Starship.Unity.Animations;
using Starship.Unity.Audio;
using Starship.Unity.Commands;
using Starship.Unity.Core;
using Starship.Unity.Enumerations;
using Starship.Unity.Events.Combat;
using Starship.Unity.Events.Models;
using Starship.Unity.Interfaces;
using Random = UnityEngine.Random;

namespace Starship.Unity.Traits {
    public class Shielded : BaseComponent, CanMitigateDamage, IsTrait {

        protected override void Start() {
            AnimationController = GetComponent<IAnimationController>();
        }

        public void MitigateDamage(Damage damage) {

            if (Durability <= 0) {
                return;
            }

            if (Random.Range(1, 100) > Chance) {
                return;
            }

            if (BlockSound != null) {
                Publish(new PlaySound(BlockSound, transform.position));
            }

            //AnimationController.Play(AnimationTypes.Block);

            var reduced = Convert.ToInt32(damage.Amount*.10f);

            switch (damage.Type) {
                case DamageTypes.Blunt:
                    Durability -= reduced;
                    break;
            }

            if (Durability > 0) {
                damage.ChangeDamage(damage.Amount - reduced, DamageTypes.Generic);
            }
        }

        public int Durability = 100;

        public int Chance = 25;

        public Sound BlockSound;

        private IAnimationController AnimationController { get; set; }
    }
}