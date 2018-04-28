using Assets.Scripts.Controls;
using Assets.Scripts.Core;
using Assets.Scripts.EventHandling.Events;
using Assets.Scripts.Extensions;
using UnityEngine;

namespace Assets.Scripts.Interaction {
    public class CreateWhileKeydown : BaseComponent {

        protected override void OnEnable() {
            On<KeyPressed>(OnKeyPressed);
        }

        private void OnKeyPressed(KeyPressed e) {
            if (e.KeyCode == Key) {
                if (e.Status == KeyStatuses.Down) {
                    Instance = this.Create(ObjectToCreate);
                }
                else if (e.Status == KeyStatuses.Up && Instance != null) {
                    Destroy(Instance);
                }
            }
        }

        public KeyCode Key;

        public GameObject ObjectToCreate;

        private GameObject Instance { get; set; }
    }
}