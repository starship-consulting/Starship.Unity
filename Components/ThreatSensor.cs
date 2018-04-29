using System;
using System.Linq;
using Starship.Unity.Actors;
using Starship.Unity.Animations;
using Starship.Unity.Combat;
using Starship.Unity.Core;
using Starship.Unity.Events;
using Starship.Unity.Events.Models;
using Starship.Unity.Scheduling;
using UnityEngine;

namespace Starship.Unity.Components {
    public class ThreatSensor : BaseComponent, IsDamageListener {

        private void CombatLoop() {
            var target = GetClosestThreat<Attackable>();

            if (target != null) {
                var animationController = GetComponent<IAnimationController>();

                if (animationController != null) {
                    //animationController.Play(AnimationTypes.Attack);
                }

                target.Attack(GetComponent<Actor>());
            }
        }
        
        private T GetClosestThreat<T>() where T : MonoBehaviour {

            foreach (var each in Threats) {
                if (each.Target != null) {
                    var component = each.Target.GetComponent<T>();

                    if (component != null) {
                        return component;
                    }
                }
            }

            return null;
        }

        public void OnDamage(Damage damage) {
            if (damage.Actor == null) {
                return;
            }

            AddThreat(damage.Actor, damage.Amount);
        }

        public void AddThreat(Actor aggressor, int value) {
            foreach (var threat in Threats) {
                if (threat.Target == aggressor) {
                    IncreaseThreat(threat, value);
                    return;
                }
            }

            var hates = Threats.ToList();
            var newThreat = new Threat(aggressor, value);
            hates.Add(newThreat);
            Threats = hates.ToArray();

            IncreaseThreat(newThreat, value);
        }

        private void IncreaseThreat(Threat threat, int value) {
            threat.Value += value;

            if (AttackTask == null) {
                AttackTask = Run(CombatLoop, TimeSpan.FromSeconds(3), true);
            }
        }

        public Threat[] Threats;

        private ScheduledTask AttackTask { get; set; }
    }
}