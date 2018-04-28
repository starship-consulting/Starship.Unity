using Assets.Scripts.Core;
using Assets.Scripts.Elements;
using Assets.Scripts.Events;
using Assets.Scripts.Events.Elements;
using Assets.Scripts.Events.Models;
using Assets.Scripts.Interaction;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.UI {
    public class Card : BaseComponent, IPointerClickHandler, IsDragDropListener, IPointerEnterHandler, IPointerExitHandler {
        
        public void OnPointerClick(PointerEventData e) {
            if (e.button == PointerEventData.InputButton.Right && !e.dragging && Item.Element.IsValidTarget(CurrentTarget)) {
                Activate(Camera.main.transform.position + Camera.main.transform.forward * 2, Camera.main.transform.rotation);
            }
        }

        public void OnDropped(DragResolution drag) {
            if (drag.Receiver == null) {
                var startPosition = Camera.main.ScreenToWorldPoint(drag.Data.position) + Camera.main.transform.forward * 2;
                var endPosition = drag.Data.pointerCurrentRaycast.worldPosition;
                var direction = (endPosition - startPosition).normalized;
                var rotation = Quaternion.LookRotation(direction);

                Activate(startPosition, rotation);
            }
            else {
                var card = drag.Receiver.GetComponent<Card>();

                if (card != null) {
                    Combine(card);
                }
            }
        }

        public void Combine(Card card) {

            if (card.Item.Element.Template == null && Item.Element.Template != null) {
                return;
            }

            Publish(new ElementCombined {
                Primary = card,
                Secondary = this
            });

            Decrease();
            card.Decrease();
        }

        public GameObject Build() {
            var template = Item.Element.CreateTemplate();
            Item.Element.ApplyTo(template);
            return template;
        }

        public void Activate(Vector3 position, Quaternion rotation) {

            Decrease();

            var nullTarget = CurrentTarget == null;

            if (nullTarget) {
                var template = Item.Element.CreateTemplate();
                template.transform.position = position;
                template.transform.rotation = rotation;

                SetTarget(template.GetComponent<Targettable>());
            }

            Item.Element.ApplyTo(CurrentTarget.gameObject);

            Publish(new ElementUsed(Item.Element));

            if (nullTarget) {
                SetTarget(null);
            }
        }

        public void Update() {
            Count.text = Item.Quantity.ToString();
        }

        public void Decrease(int count = 1) {
            Item.Quantity -= count;
            Count.text = Item.Quantity.ToString();
        }

        public void SetTarget(Targettable target) {
            CurrentTarget = target;
        }

        public void Set(InventoryItem item) {
            Title.text = item.Element.Title;
            Description.text = item.Element.Description;

            if (item.Element.Graphic != null) {
                Graphic.texture = item.Element.Graphic.texture;
            }
            
            Icon.sprite = item.Element.Icon;
            Count.text = item.Quantity.ToString();
            Item = item;
        }

        public void OnPointerEnter(PointerEventData e) {
            Publish(new CardHovered {
                Card = this
            });
        }

        public void OnPointerExit(PointerEventData e) {
            Publish(new CardUnhovered {
                Card = this
            });
        }

        public Element GetElement() {
            return Item.Element;
        }

        public Text Title;

        public Text Description;

        public Text Count;
        
        public Image Icon;

        public RawImage Graphic;

        public InventoryItem Item;

        public Quaternion GraphicRotation { get; set; }

        public bool UseAnimatedGraphic { get; set; }

        private Targettable CurrentTarget { get; set; }
    }
}