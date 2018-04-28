using System.Collections.Generic;

namespace Assets.Scripts.Elements {
    public abstract class AbilityFilter {
        public abstract List<Element> Filter(List<Element> cards);
    }
}