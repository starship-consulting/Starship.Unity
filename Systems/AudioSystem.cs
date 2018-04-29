using Starship.Unity.Audio;
using Starship.Unity.Commands;
using Starship.Unity.Core;
using Starship.Unity.Extensions;
using UnityEngine;

namespace Starship.Unity.Systems {
    public class AudioSystem : BaseComponent {
        protected override void OnEnable() {
            base.OnEnable();

            On<PlaySound>(OnPlaySound);
        }

        private void OnPlaySound(PlaySound e) {
            if (e.Collision != null) {
                PlayCollision(e);
            }
            else {
                Play(e);
            }
        }

        public AudioClip PlayCollision(PlaySound e) {
            var clip = GetClip(e);

            if (clip == null) {
                return null;
            }

            var audioSource = GetAudioSource(clip, e, e.Collision.transform.position);

            audioSource.pitch = 6f - (e.Collision.relativeVelocity.magnitude/20);
            audioSource.volume = 0.5f*(e.Collision.relativeVelocity.magnitude/20)*e.Sound.Volume;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.maxDistance = 30;
            audioSource.spatialBlend = 1;

            return clip;
        }

        public AudioClip Play(PlaySound e) {
            var clip = GetClip(e);

            if (clip == null) {
                return null;
            }

            var source = GetAudioSource(clip, e, e.Position);
            source.loop = e.Sound.Loop;
            source.pitch = Random.Range(e.Sound.MinPitch, e.Sound.MaxPitch);
            source.volume = e.Sound.Volume;

            return clip;
        }

        public AudioClip GetClip(PlaySound e) {
            if (e.Sound.Clips.Length == 0) {
                return null;
            }

            return e.Sound.Clips.Length == 1 ? e.Sound.Clips[0] : e.Sound.Clips.GetRandom();
        }

        private AudioSource GetAudioSource(AudioClip clip, PlaySound e, Vector3 position) {
            var source = this.Create<TemporaryAudioSource>("(Sound) " + e.Sound.name);
            source.transform.position = position;
            source.Clip = new [] { clip };

            var audiosource = source.GetComponent<AudioSource>();
            audiosource.spatialBlend = e.Sound.SpatialBlend;
            audiosource.maxDistance = 30;
            audiosource.rolloffMode = AudioRolloffMode.Linear;

            return audiosource;
        }
    }
}