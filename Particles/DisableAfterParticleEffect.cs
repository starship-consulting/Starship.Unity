using System;
using Starship.Unity.Core;
using UnityEngine;

namespace Starship.Unity.Particles {
    public class DisableAfterParticleEffect : BaseComponent {

        protected override void OnEnable() {
            base.OnEnable();

            var particle = GetComponent<ParticleSystem>();

            Run(() => {
                gameObject.SetActive(false);
            }, TimeSpan.FromSeconds(particle.duration));
        }
    }
}