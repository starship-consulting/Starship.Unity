using System;
using Assets.Scripts.Core;
using Assets.Scripts.Events.Interaction;
using Assets.Scripts.Extensions;
using UnityEngine;

namespace Assets.Scripts.Interaction {
    public class InteractionReceiver : BaseComponent {

        protected override void OnEnable() {
            base.OnEnable();

            On<RequestInteract>(OnRequestInteraction);
        }

        private void OnRequestInteraction(RequestInteract e) {
            if(SourceResult != null) {
                e.Source.Create(SourceResult);
            }

            if(TargetResult != null) {
                e.Target.Create(TargetResult);
            }
        }

        public GameObject SourceResult;

        public GameObject TargetResult;
    }
}