using System;
using Assets.Scripts.Attributes;
using Assets.Scripts.Components;
using Assets.Scripts.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Layouts {

    [Require(typeof(GridLayoutGroup))]
    [Require(typeof(ContentSizeFitter))]
    public class GridPresenter : EditableComponent {

        protected override void Start() {
            base.Start();
            
            ContentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.MinSize;
            ContentSizeFitter.verticalFit = ContentSizeFitter.FitMode.MinSize;
        }

        protected override void UpdateState() {

            this.ClearChildren();

            for (var width = 1; width <= Width; width++) {
                for (var height = 1; height <= Height; height++) {
                    this.Create(ItemTemplate, ItemTemplate.name + "_" + width + "_" + height);
                }
            }

            var child = this.GetFirstChild();

            if(child != null) {
                var size = child.GetComponent<RectTransform>();
                GridLayoutGroup.cellSize = size.sizeDelta;
            }

            GridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            GridLayoutGroup.constraintCount = Width;
            GridLayoutGroup.padding = Padding;
            GridLayoutGroup.spacing = CellPadding;
        }

        public int Width;
        
        public int Height;

        public RectOffset Padding;

        public Vector2 CellPadding;

        public GameObject ItemTemplate;

        private ContentSizeFitter ContentSizeFitter;

        private GridLayoutGroup GridLayoutGroup;
    }
}