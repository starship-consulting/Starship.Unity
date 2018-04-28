using Assets.Scripts.Attributes;
using Assets.Scripts.Core;
using HighlightingSystem;
using UnityEngine;

namespace Assets.Scripts.Effects {

    [DisallowMultipleComponent, Require(typeof(Highlighter))]
    public class HighlightOccluder : BaseComponent {

        protected override void OnEnable() {
            base.OnEnable();
            Highlighter.occluder = true;
            Highlighter.seeThrough = true;
        }

        private Highlighter Highlighter { get; set; }
    }
}