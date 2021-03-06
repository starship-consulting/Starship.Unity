﻿using System.Collections.Generic;
using Starship.Unity.Entities;
using UnityEngine;

namespace Starship.Unity.Components {
    public class EntityController : MonoBehaviour {

        private void Awake() {
            Containers = new Dictionary<string, Transform>();
            //Entity.Created += OnEntityCreated;
        }

        private void OnEntityCreated(Entity entity) {
            var name = entity.GetType().Name + "s";
            Transform container = null;

            if (!Containers.ContainsKey(name)) {
                container = new GameObject(name).transform;
                container.parent = transform;
                Containers.Add(name, container);
            }

            entity.transform.parent = container;
        }

        private Dictionary<string, Transform> Containers { get; set; } 
    }
}