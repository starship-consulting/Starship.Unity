using System;
using System.Linq;
using Assets.Scripts.Core;

namespace Assets.Scripts.Editor.Editors {
    public class SerializableEventInfoEditor : BaseCustomEditor<SerializableEventInfo> {

        public override void Draw(SerializableEventInfo model) {
            if (model.Source != null) {
                var events = model.Source.GetType().GetEvents();
                var dictionary = events.ToDictionary(each => each.Name, each => each.Name);
                Dropdown(dictionary, property => property.EventName);
            }
        }
    }
}