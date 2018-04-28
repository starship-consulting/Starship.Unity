using Assets.Scripts.Core;
using Assets.Scripts.Crafting;
using Assets.Scripts.Interfaces;
using Assets.Scripts.UI.Layouts;
using UnityEngine;

namespace Assets.Scripts.UI.Crafting {
    public class CraftPresenter : BaseComponent, IsSelectionListener {

        public void OnItemSelected(MonoBehaviour source) {
        }

        public IconLayoutPresenter RecipeSelector;

        public GameObject RecipeAssembler;

        public Craft SelectedCraft;
    }
}