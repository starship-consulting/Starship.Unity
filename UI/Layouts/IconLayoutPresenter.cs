﻿using Assets.Scripts.Core;
using Assets.Scripts.UI.Simple;

namespace Assets.Scripts.UI.Layouts {
    public class IconLayoutPresenter : BaseComponent {
        
        /*protected override void UpdateBoundState() {
            
            foreach (var item in Items) {
                var instance = this.Create(Template);
                instance.Text.text = item.ToString();
                instance.Image.sprite = item.GetIcon();
                instance.Source = item as MonoBehaviour;
            }
        }*/
        
        public SelectableLabelledIcon Template;

        public LabelledIcon[] Icons;
    }
}