using System;
using System.Collections;
using Assets.Scripts.Extensions;
using UnityEngine;

namespace Assets.Scripts.Commands {
    public class DelayedCommand : Command {

        protected override void Start() {
            base.Start();

            if (Action == null) {
                this.Destroy();
                return;
            }

            StartCoroutine(DoCommand());
        }

        private IEnumerator DoCommand() {
            yield return new WaitForFixedUpdate();
            Action();
            Finish();
        }

        public Action Action { get; set; }
    }
}