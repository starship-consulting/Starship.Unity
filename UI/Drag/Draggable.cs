using Assets.Scripts.Core;
using Assets.Scripts.Events;
using Assets.Scripts.Events.Models;
using Assets.Scripts.Events.UI;
using Assets.Scripts.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI.Drag {
    public class Draggable : BaseComponent, IBeginDragHandler, IEndDragHandler, IDragHandler {

        public void OnBeginDrag(PointerEventData e) {
            if (e.button != PointerEventData.InputButton.Left) {
                return;
            }

            var canvas = this.FindInParents<Canvas>();

            if (canvas == null) {
                return;
            }

            OriginalParent = transform.parent;
            OriginalPosition = transform.position;

            transform.parent = canvas.transform;

            Dragging = this.AddComponent<Dragging>();
        }

        public void OnEndDrag(PointerEventData e) {
            if (Dragging == null) {
                return;
            }

            transform.parent = OriginalParent;
            transform.position = OriginalPosition;

            if (Dragging.DropZone == null) {

                Publish(new Dropped {
                    Source = this,
                    Position = e.position
                });
            }
            else {
                //transform.parent = Dragging.DropZone.transform;

                Publish(new Dropped {
                    Source = this,
                    Position = Dragging.DropZone.transform.position,
                    Receiver = Dragging.DropZone
                });
            }

            Publish<IsDragDropListener>(listener => listener.OnDropped(new DragResolution {
                Data = e,
                Receiver = Dragging.DropZone
            }));

            transform.localPosition = Vector3.zero;

            this.Remove<Dragging>();
        }

        public void OnDrag(PointerEventData e) {
        }

        private Dragging Dragging { get; set; }

        private Transform OriginalParent { get; set; }

        private Vector3 OriginalPosition { get; set; }
    }
}