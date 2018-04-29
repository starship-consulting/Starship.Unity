using System;
using Starship.Unity.Core;
using Starship.Unity.Entities;
using UnityEngine;

namespace Starship.Unity.Components {
    public class EntitySpawner : BaseComponent {

        protected override void Start() {
            base.Start();

            if (Delay > 0) {
                Run(Spawn, TimeSpan.FromSeconds(Delay));
            }
            else {
                Spawn();
            }
        }

        private void Spawn() {
            var entity = Instantiate(GameObject);

            entity.transform.position = transform.position + Offset;
            entity.transform.rotation = transform.rotation;

            Destroy(gameObject, DestroyAfter);
        }

        public Entity GameObject;

        public float Delay = 0;

        public float DestroyAfter = 0;

        public Vector3 Offset = Vector3.zero;
    }
}