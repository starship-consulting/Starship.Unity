using System;
using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.Particles {
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