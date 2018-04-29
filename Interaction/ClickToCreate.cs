using Starship.Unity.Audio;
using Starship.Unity.Commands;
using Starship.Unity.Controls;
using Starship.Unity.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Starship.Unity.Interaction {
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