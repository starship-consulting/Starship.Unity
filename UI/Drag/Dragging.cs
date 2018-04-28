using Assets.Scripts.Core;
using Assets.Scripts.Events.UI;
using Assets.Scripts.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Drag {
    public class Dragging : BaseComponent, IDragHandler {

        protected override void OnEnable() {
            base.OnEnable();

            Canvas = this.FindInParents<Canvas>();
            Rect = GetComponent<RectTransform>();
            
            this.Disable<LayoutElement>();

            this.With<CanvasGroup>(each => {
                each.blocksRaycasts = false;
            });

            On<EnteredDragDropZone>(e => {
                DropZone = e.Zone;
            });

            On<ExitedDragDropZone>(e => {
                DropZone = null;
            });
        }

        protected override void OnStopped() {
            base.OnStopped();
            this.Enable<LayoutElement>();

            this.With<CanvasGroup>(each => {
                each.blocksRaycasts = true;
            });
        }

        public void OnDrag(PointerEventData e) {

            var plane = Canvas.transform as RectTransform;
            
            Vector3 mousePosition;

            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(plane, e.position, e.pressEventCamera, out mousePosition)) {
                Rect.position = new Vector3(mousePosition.x, mousePosition.y, 1f);
                Rect.rotation = plane.rotation;
            }
        }

        public DragDropReceiver DropZone;

        private Canvas Canvas { get; set; }

        private RectTransform Rect { get; set; }
    }
}