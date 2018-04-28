using System;
using Assets.Scripts.Attributes;
using Assets.Scripts.Audio;
using Assets.Scripts.Commands;
using Assets.Scripts.Core;
using Assets.Scripts.Events;
using Assets.Scripts.Extensions;
using Assets.Scripts.Interaction;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Entities {
    
    public class Item : BaseComponent, IPointerClickHandler {

        protected override void OnEnable() {
            base.OnEnable();
            
            var targettable = this.GetOrAdd<Targettable>();
            targettable.CanSelect = false;
            targettable.Tooltip = Tooltip;
        }

        public void OnPointerClick(PointerEventData e) {
            return;
            Publish(new ItemGrabbed(this));
            gameObject.SetActive(false);
        }

        protected void OnCollisionEnter(Collision collision) {
            if (collision.relativeVelocity.magnitude <= 2) {
                return;
            }

            if (CollisionSound != null) {
                Publish(new PlaySound(CollisionSound) { Collision = collision });
            }
        }

        public void Drop() {
            return;
            var baseStrength = 20;
            var percent = (Input.mousePosition.y + 1)/Screen.height;
            var strength = Math.Max((baseStrength*percent) + 1, 10);

            var startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var targetPosition = Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(20);

            transform.position = startPosition;
            gameObject.SetActive(true);

            var direction = (targetPosition - startPosition);
            direction.Normalize();

            GetComponent<Rigidbody>().AddForce(direction*strength, ForceMode.Impulse);

            if (ThrowSound != null) {
                Publish(new PlaySound(ThrowSound, startPosition));
            }

            Publish(new ItemDropped(this));
        }

        public string Tooltip;

        public Sprite Icon;

        public Sound CollisionSound;

        public Sound ThrowSound;

        public int Slot;
    }
}