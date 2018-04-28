using System.Collections.Generic;
using Assets.Scripts.Core;
using Assets.Scripts.Extensions;
using UnityEngine;

namespace Assets.Scripts.UI {
    
    public class Repeater : BaseComponent {

        protected override void OnEnable() {
            base.OnEnable();
            UpdateState();
        }

        public IEnumerable<GameObject> SetCount(int count) {
            Count = count;
            return UpdateState();
        }
        
        public IEnumerable<GameObject> UpdateState() {
            if (Template == null) {
                yield return null;
            }

            this.ClearChildren();

            for (var count = 0; count < Count; count++) {
                yield return this.Create(Template);
            }
        }

        public int Count;

        public GameObject Template;
    }
}