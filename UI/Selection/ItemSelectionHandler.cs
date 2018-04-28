using Assets.Scripts.Core;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.UI.Selection {
    public class ItemSelectionHandler : BaseComponent, IsSelectionListener {

        public void OnItemSelected(MonoBehaviour source) {
            //OnSelected.Invoke(source);
        }

        public SerializableType ExpectedType;

        //public ActionBinding OnSelected;
    }
}