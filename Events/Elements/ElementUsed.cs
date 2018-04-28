using Assets.Scripts.Elements;

namespace Assets.Scripts.Events.Elements {
    public class ElementUsed {
        public ElementUsed() {
        }

        public ElementUsed(Element element) {
            Element = element;
        }

        public Element Element { get; set; }
    }
}