using System;
using System.Linq;
using Starship.Unity.Core;
using Starship.Unity.Interfaces;
using Starship.Unity.UI;

namespace Starship.Unity.Crafting {
    
    public class CraftController : BaseComponent {

        protected override void Start() {
            base.Start();

            if (CraftsUpdated != null) {
                CraftsUpdated(Crafts);
            }
        }

        public void Fill(DataContext<HasIcon> context) {
            context.Set(Crafts.Cast<HasIcon>().ToArray());
        }

        public Craft[] Crafts;

        public event Action<Craft[]> CraftsUpdated;
    }
}