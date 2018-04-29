using Starship.Unity.Core;
using UnityEditor;

namespace Starship.Unity.Editor.PropertyDrawers {

    [CustomPropertyDrawer(typeof(EventSubscriber))]
    public class EventSubscriberPropertyDrawer : BasePropertyDrawer<EventSubscriber> {

        protected override void Update() {
            using (Property()) {
                Label();
            }
        }
    }
}