using System;
using Starship.Unity.Utilities;

namespace Starship.Unity.Core {
    public static class EventHub {

        static EventHub() {
            Routes = new TypeRouter();
        }

        public static void Publish<E>(E e) {
            Routes.Publish(e);

            if (EventPublished != null) {
                EventPublished(e);
            }
        }

        public static Guid On<E>(Action<E> callback) {
            return Routes.On(callback);
        }

        public static void Off(Guid id) {
            Routes.Off(id);
        }

        public static event Action<object> EventPublished;

        private static TypeRouter Routes { get; set; }
    }
}
