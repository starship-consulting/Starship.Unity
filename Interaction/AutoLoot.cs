using System.Linq;
using Starship.Unity.Core;
using Starship.Unity.Elements;
using Starship.Unity.Extensions;
using Starship.Unity.Movement;
using UnityEngine;

namespace Starship.Unity.Interaction {

    [RequireComponent(typeof(Inventory))]
    public class AutoLoot : BaseComponent {

        protected override void Awake() {
            base.Awake();
            Inventory = GetComponent<Inventory>();
            Movement = GetComponent<IsMovementController>();
        }

        protected override void OnEnable() {
            base.OnEnable();
            Movement.OnFinishedMoving += OnFinishedMoving;
        }
        
        private void OnFinishedMoving() {
            var nearby = this.FindAll<Lootable>().Where(each => transform.Within(each.transform, LootDistance)).ToList();

            foreach (var lootable in nearby) {
                Inventory.Add(lootable.Element);
                lootable.gameObject.Delete();
            }
        }

        public float LootDistance = 5;

        private Inventory Inventory { get; set; }

        private IsMovementController Movement { get; set; }
    }
}