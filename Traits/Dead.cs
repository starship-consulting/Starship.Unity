using System;
using System.Collections.Generic;
using System.Linq;
using Starship.Unity.Animations;
using Starship.Unity.Core;
using Starship.Unity.Extensions;
using Starship.Unity.Interfaces;
using UnityEngine;

namespace Starship.Unity.Traits {
    public class Dead : BaseComponent, IsTrait {

        protected override void Awake() {
            base.Awake();
            AnimationController = GetComponent<IAnimationController>();
        }

        protected override void Start() {
            base.Start();

            var colliderToRemove = GetComponent<Collider>();
            this.Remove(colliderToRemove);

            foreach (MonoBehaviour component in GetComponents<MonoBehaviour>()) {
                if (FilterTypes.Any(type => component.GetType().IsAssignableFrom(type))) {
                    continue;
                }

                this.Remove(component);
            }

            if (AnimationController != null) {
                //AnimationController.Play(AnimationTypes.Death);
            }
        }

        private static readonly List<Type> FilterTypes = new List<Type> {
            typeof(Dead),
            typeof(Animation),
            typeof(IAnimationController)
        };

        private IAnimationController AnimationController { get; set; }
    }
}