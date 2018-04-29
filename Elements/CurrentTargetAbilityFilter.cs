using System.Collections.Generic;
using System.Linq;
using Starship.Unity.Interaction;

namespace Starship.Unity.Elements {
    public class CurrentTargetAbilityFilter : AbilityFilter {
        public CurrentTargetAbilityFilter(Targettable target) {
            Target = target;
        }

        public override List<Element> Filter(List<Element> abilities) {
            if (Target == null) {
                return abilities;
            }

            return abilities.Where(each => each.IsValidTarget(Target)).ToList();
        }

        public Targettable Target { get; set; }
    }
}