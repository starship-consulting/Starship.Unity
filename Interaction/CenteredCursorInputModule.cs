using System.Reflection;
using Starship.Unity.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Starship.Unity.Interaction {
    public class CenteredCursorInputModule : StandaloneInputModule {

        protected override void Awake() {
            base.Awake();

            MouseStateField = this.GetPrivateField("m_MouseState");
        }

        protected override MouseState GetMousePointerEventData(int id) {
            PointerEventData data1;

            bool pointerData = GetPointerData(-1, out data1, true);

            data1.Reset();

            if (pointerData) {
                data1.position = input.mousePosition;
            }

            Vector2 mousePosition = input.mousePosition;

            data1.delta = mousePosition - data1.position;
            data1.position = mousePosition;

            data1.scrollDelta = input.mouseScrollDelta;
            data1.button = PointerEventData.InputButton.Left;
            eventSystem.RaycastAll(data1, m_RaycastResultCache);
            RaycastResult firstRaycast = FindFirstRaycast(m_RaycastResultCache);
            data1.pointerCurrentRaycast = firstRaycast;
            m_RaycastResultCache.Clear();
            PointerEventData data2;
            GetPointerData(-2, out data2, true);
            CopyFromTo(data1, data2);
            data2.button = PointerEventData.InputButton.Right;
            PointerEventData data3;
            GetPointerData(-3, out data3, true);
            CopyFromTo(data1, data3);
            data3.button = PointerEventData.InputButton.Middle;
            
            var state = MouseStateField.GetValue(this) as MouseState;

            state.SetButtonState(PointerEventData.InputButton.Left, StateForMouseButton(0), data1);
            state.SetButtonState(PointerEventData.InputButton.Right, StateForMouseButton(1), data2);
            state.SetButtonState(PointerEventData.InputButton.Middle, StateForMouseButton(2), data3);

            return state;
        }

        protected override void ProcessMove(PointerEventData e) {
            var target = e.pointerCurrentRaycast.gameObject;
            HandlePointerExitAndEnter(e, target);
        }

        private FieldInfo MouseStateField { get; set; }
    }
}