using Starship.Unity.Core;
using Starship.Unity.Extensions;
using UnityEngine;

namespace Starship.Unity.Utility {

    [ExecuteInEditMode]
    public class RevealComponents : BaseComponent {

        protected override void OnEnable() {
            base.OnEnable();

            foreach (var component in this.GetAllComponents()) {
                component.hideFlags = HideFlags.None;
            }
        }
    }
}