using Assets.Scripts.Core;
using Assets.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Simple {
    public class SelectableLabelledIcon : BaseComponent, IPointerClickHandler, CanBind<HasIcon> {

        public void Bind(HasIcon data) {
            Text.text = data.ToString();
            Image.sprite = data.Icon;
        }

        public void OnPointerClick(PointerEventData e) {
            ExecuteEvents.ExecuteHierarchy<IsSelectionListener>(gameObject, e, (target, data) => target.OnItemSelected(Source));
        }

        public Text Text;

        public Image Image;

        public MonoBehaviour Source;
    }
}