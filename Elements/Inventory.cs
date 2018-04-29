using System.Collections.Generic;
using System.Linq;
using Starship.Unity.Core;
using Starship.Unity.Events.Elements;

namespace Starship.Unity.Elements {
    public class Inventory : BaseComponent {

        public void Add(Element element) {
            var item = Items.FirstOrDefault(each => each.Element.Equals(element));

            if (item != null) {
                item.Quantity += 1;
            }
            else {
                item = new InventoryItem {
                    Element = element,
                    Quantity = 1
                };

                Items.Add(item);
            }

            Publish(new ElementAdded {
                Item = item,
                Inventory = this
            });
        }

        public IEnumerable<Recipe> GetRecipes() {
            return Items.Select(each => each.Element).OfType<Recipe>();
        }

        public List<InventoryItem> GetInventoryItems() {
            return Items.ToList();
        }

        public List<InventoryItem> Items;
    }
}