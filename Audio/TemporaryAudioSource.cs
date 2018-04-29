using System.Collections;
using Starship.Unity.Core;
using Starship.Unity.Extensions;
using UnityEngine;

namespace Starship.Unity.Audio {
    public class TemporaryAudioSource : BaseComponent {

        protected override void Awake() {
            base.Awake();
            Source = this.AddComponent<AudioSource>();
        }

        protected override void Start() {
            StartCoroutine(Play());
        }

        private IEnumerator Play() {
            if (Delay > 0) {
                yield return new WaitForSeconds(Delay/1000);
            }

            var clip = Clip.GetRandom();

            if (clip != null) {
                //Source.PlayOneShot(Clip);
                Source.clip = clip;
                Source.Play();

                if (!Source.loop) {
                    DestroyObject(gameObject, clip.length);
                }
            }
            else {
                DestroyObject(gameObject);
            }
        }

        public float Delay;

        public AudioClip[] Clip;

        private AudioSource Source;
    }
}