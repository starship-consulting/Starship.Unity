using Assets.Scripts.Core;
using Assets.Scripts.Extensions;
using UnityEngine;

namespace Assets.Scripts.Utility {

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