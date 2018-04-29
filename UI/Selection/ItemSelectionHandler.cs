using Starship.Unity.Core;
using Starship.Unity.Interfaces;
using UnityEngine;

namespace Starship.Unity.UI.Selection {
    public class ItemSelectionHandler : BaseComponent, IsSelectionListener {

        public void OnItemSelected(MonoBehaviour source) {
            //OnSelected.Invoke(source);
        }

        public SerializableType ExpectedType;

        //public ActionBinding OnSelected;
    }
}