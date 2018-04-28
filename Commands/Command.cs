using System;
using System.Collections;
using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.Commands {
    public abstract class Command : BaseComponent {
        
        public virtual void Undo() {
        }

        public void SetProgress(float progress) {
            if (ProgressChanged != null) {
                ProgressChanged(this, progress);
            }

            if (progress >= 1) {
                Finish();
            }
        }

        public void Finish() {
            StartCoroutine(Finished());
        }

        private IEnumerator Finished() {
            yield return new WaitForEndOfFrame();
            Destroy(this);
        }

        public event Action<Command, float> ProgressChanged;

        public bool DestroyWhenFinished = true;
    }
}