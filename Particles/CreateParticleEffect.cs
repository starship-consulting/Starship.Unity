using Starship.Unity.Core;
using Starship.Unity.Extensions;
using UnityEngine;

namespace Starship.Unity.Particles {
    public class CreateParticleEffect : BaseComponent {

        protected override void Start() {
            base.Start();
            ParticleSystemInstance = this.Create(ParticleEffect.gameObject);
            
            var particles = ParticleSystemInstance.FindComponents<ParticleSystem>();
            float duration = 0;

            foreach (var particle in particles) {
                if (duration < particle.duration) {
                    duration = particle.duration;
                }
            }

            var sounds = ParticleSystemInstance.FindComponents<AudioSource>();

            foreach (var source in sounds) {
                if (duration < source.clip.length) {
                    duration = source.clip.length;
                }
            }

            if (DestroyAfterPlay) {
                Destroy(ParticleSystemInstance, duration);
                Destroy(this, duration);
            }
        }

        public GameObject ParticleEffect;

        public bool DestroyAfterPlay = true;

        private GameObject ParticleSystemInstance { get; set; }
    }
}