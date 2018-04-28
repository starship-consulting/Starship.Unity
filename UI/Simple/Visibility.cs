using Assets.Scripts.Attributes;
using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.UI.Simple {
    
    [ExecuteInEditMode, Require(typeof(CanvasGroup))]
    public class Visibility : BaseComponent {

        public void Show() {
            IsVisible = true;
            //DataChanged();
        }

        public void Hide() {
            IsVisible = false;
            //DataChanged();
        }

        public void Toggle() {
            IsVisible = !IsVisible;
        }

        /*public override void DataChanged() {
            CanvasGroup.alpha = IsVisible ? 1 : 0;
            CanvasGroup.interactable = IsVisible;
            CanvasGroup.blocksRaycasts = IsVisible;
        }*/
        
        public bool IsVisible;
        
        private CanvasGroup CanvasGroup { get; set; }
    }
}