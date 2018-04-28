using System;
using Assets.Scripts.Core;
using UnityEditor;

namespace Assets.Scripts.Editor.PropertyDrawers {

    [CustomPropertyDrawer(typeof(EventSubscriber))]
    public class EventSubscriberPropertyDrawer : BasePropertyDrawer<EventSubscriber> {

        protected override void Update() {
            using (Property()) {
                Label();
            }
        }
    }
}