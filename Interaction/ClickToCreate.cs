using Assets.Scripts.Audio;
using Assets.Scripts.Commands;
using Assets.Scripts.Controls;
using Assets.Scripts.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Interaction {
    public class ClickToCreate : BaseComponent, IPointerDownHandler {
        public void OnPointerDown(PointerEventData e) {
            var target = Distance > 0 ? Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(Distance) : MouseHelper.Raycast().point;

            if (ObjectToCreate != null) {
                Instantiate(ObjectToCreate, target, Quaternion.identity);
            }

            if (Sound != null) {
                Publish(new PlaySound(Sound));
            }
        }

        public GameObject ObjectToCreate;

        public float Distance;

        public Sound Sound;
    }
}