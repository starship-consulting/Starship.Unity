using Assets.Scripts.Core;
using Assets.Scripts.Events.Elements;
using Assets.Scripts.Extensions;

namespace Assets.Scripts.Elements {
    public class RecipeController : BaseComponent {

        protected override void OnEnable() {
            base.OnEnable();

            On<ElementCombined>(OnElementCombined);
        }

        private void OnElementCombined(ElementCombined e) {

            foreach (var each in GetComponentsInChildren<Recipe>()) {
                if (each.Primary == e.Primary.GetElement() && each.Secondary == e.Secondary.GetElement()) {
                    Inventory.Add(each);
                    return;
                }
            }

            var primary = e.Primary.GetElement();
            var secondary = e.Secondary.GetElement();
            
            var recipe = this.Create<Recipe>();
            recipe.Title = primary.Title;
            recipe.Description = primary.Description;
            recipe.Graphic = primary.Graphic;
            recipe.Icon = primary.Icon;
            recipe.Template = primary.Template;
            recipe.PreviewOutline = primary.PreviewOutline;
            recipe.PreviewDistance = primary.PreviewDistance;

            recipe.Primary = primary;
            recipe.Secondary = secondary;

            recipe.UpdateData();

            Inventory.Add(recipe);
        }

        public Inventory Inventory;
    }
}