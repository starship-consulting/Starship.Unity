using Starship.Unity.Enumerations;
using UnityEngine;

namespace Starship.Unity.UI.Tooltips {
    public struct TooltipModel {

        public PropertyTypes Type { get; set; }

        public GameObject Target { get; set; }

        public Sprite Icon { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}