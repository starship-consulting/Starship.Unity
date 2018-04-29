using System;

namespace Starship.Unity.Core {

    [Serializable]
    public class EventSubscriber {
        public EventSubscriber() {
        }

        public EventSubscriber(Type type) {
            Type = new SerializableType(type);
        }

        public SerializableType Type;
    }
}