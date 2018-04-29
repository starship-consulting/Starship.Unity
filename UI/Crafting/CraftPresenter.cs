using Starship.Unity.Core;
using Starship.Unity.Crafting;
using Starship.Unity.Interfaces;
using Starship.Unity.UI.Layouts;
using UnityEngine;

namespace Starship.Unity.UI.Crafting {
    public class CraftPresenter : BaseComponent, IsSelectionListener {

        public void OnItemSelected(MonoBehaviour source) {
        }

        public IconLayoutPresenter RecipeSelector;

        public GameObject RecipeAssembler;

        public Craft SelectedCraft;
    }
}