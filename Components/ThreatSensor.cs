using System;
using System.Linq;
using Assets.Scripts.Actors;
using Assets.Scripts.Animations;
using Assets.Scripts.Combat;
using Assets.Scripts.Core;
using Assets.Scripts.Enumerations;
using Assets.Scripts.Events;
using Assets.Scripts.Events.Models;
using Assets.Scripts.Scheduling;
using UnityEngine;

namespace Assets.Scripts.Components {
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