using Starship.Unity.Core;
using Starship.Unity.Events.Interaction;
using Starship.Unity.Extensions;
using UnityEngine;

namespace Starship.Unity.Interaction {
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