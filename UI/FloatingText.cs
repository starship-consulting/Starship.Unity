using Assets.Scripts.Core;
using EckTechGames.FloatingCombatText;
using UnityEngine;

namespace Assets.Scripts.UI {
    public class FloatingText : BaseComponent {

        public void Display(object text) {
            OverlayCanvasController.instance.ShowCombatText(gameObject, CombatTextType.Hit, text.ToString());
        }

        public void Display(object text, Color color, CombatTextType type = CombatTextType.Hit) {
            OverlayCanvasController.instance.ShowCombatText(gameObject, type, text.ToString());
        }
    }
}