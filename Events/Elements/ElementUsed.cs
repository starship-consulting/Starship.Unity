using Starship.Unity.Elements;

namespace Starship.Unity.Events.Elements {
    public class ElementUsed {
        public ElementUsed() {
        }

        public ElementUsed(Element element) {
            Element = element;
        }

        public Element Element { get; set; }
    }
}