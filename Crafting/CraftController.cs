using System;
using System.Linq;
using Assets.Scripts.Core;
using Assets.Scripts.Interfaces;
using Assets.Scripts.UI;

namespace Assets.Scripts.Crafting {
    
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