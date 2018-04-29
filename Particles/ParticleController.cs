using System;
using Starship.Unity.Core;
using Starship.Unity.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace Starship.Unity.Particles {
    public class ParticleController : BaseComponent {

        public void PlayOneMoreLoop() {
            foreach (var system in GetComponentsInChildren<ParticleSystem>()) {
                system.loop = false;
            }

            this.Routine(ReviewParticleState, TimeSpan.FromSeconds(1));
        }

        private void ReviewParticleState() {
            foreach (var system in GetComponentsInChildren<ParticleSystem>()) {
                if (system.IsAlive()) {
                    this.Routine(ReviewParticleState, TimeSpan.FromSeconds(1));
                    return;
                }
            }

            OnParticlesFinished.Invoke();
        }
        
        public UnityEvent OnParticlesFinished;
    }
}