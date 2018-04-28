using System;
using System.Linq;
using Assets.Scripts.Activities;
using Assets.Scripts.Animations;
using Assets.Scripts.Combat;
using Assets.Scripts.Computations;
using Assets.Scripts.Core;
using Assets.Scripts.Effects;
using Assets.Scripts.Extensions;
using Assets.Scripts.Scheduling;
using UnityEngine;

namespace Assets.Scripts.Actors {
    public class Actor : BaseComponent {
        
        protected override void OnEnable() {
            base.OnEnable();

            /*CurrentAttack = new Attack {
                Range = 2
            };*/
        }

        protected override void Start() {
            base.Start();

            this.GetOrAdd<MecanimAnimationController>();

            InitializeEffects();

            /*var state = ActorType.GetDefaultState();

            if (state != null) {
                this.Create(ActorType.Mesh);

                //Activities = this.GetOrAdd<ActivityController>();
                Animation = Add<MecanimAnimationController>();
            }*/
        }

        public float Compute(Algorithm algorithm) {
            return algorithm.ComputeValue(this);
        }
        
        public void Attack(Attackable target) {
            if (CurrentCombatTarget == target) {
                return;
            }

            if (target != CurrentCombatTarget) {
                GetTaskScheduler().Stop(CombatTask);
            }

            CurrentCombatTarget = target;

            if (target == null) {
                return;
            }

            CombatTask = Run(Strike, TimeSpan.FromSeconds(1));
        }

        public Effect AddEffect(Effect effect) {
            foreach (var each in GetComponentsInChildren<Effect>()) {
                if (each.Name == effect.Name) {
                    return each;
                }
            }

            return this.Create(effect);
        }

        private void InitializeEffects() {
            Effects = GetComponentsInChildren<Effect>().ToArray();

            //var effects = new List<Effect>();

            /*foreach (var effect in Effects) {
                var result = EffectsContainer.Create(effect, effect.Name);
                //result.gameObject.SetActive(false);
                //effects.Add(result);
            }*/

            /*NextFrame(() => {
                foreach (var effect in effects) {
                    effect.gameObject.SetActive(true);
                }
            });*/
        }

        private void Strike() {
            if (CurrentCombatTarget != null) {
                //Abilities.First().Use(CurrentCombatTarget.gameObject);
                CombatTask = Run(Strike, TimeSpan.FromSeconds(1));
            }
        }
        
        public AudioClip[] FootstepSounds;

        public float Height = 2.5f;
        
        public string Name;

        //public Element[] Abilities;

        //public List<OldEffect> Effects;

        public Sprite Portrait;

        //public Attack CurrentAttack;

        //public CharacterProperty[] Properties;
        
        private ActivityController Activities;

        private ScheduledTask CombatTask;

        public Attackable CurrentCombatTarget;

        private Effect[] Effects;
    }
}