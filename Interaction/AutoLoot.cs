using System.Linq;
using Assets.Scripts.Core;
using Assets.Scripts.Elements;
using Assets.Scripts.Extensions;
using Assets.Scripts.Movement;
using UnityEngine;

namespace Assets.Scripts.Interaction {

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