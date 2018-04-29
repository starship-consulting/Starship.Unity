using System.Collections.Generic;

namespace Starship.Unity.Elements {
    public abstract class AbilityFilter {
        public abstract List<Element> Filter(List<Element> cards);
    }
}