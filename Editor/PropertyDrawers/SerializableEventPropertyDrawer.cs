using System.Linq;
using Starship.Unity.Core;
using UnityEditor;

namespace Starship.Unity.Editor.PropertyDrawers {

    [CustomPropertyDrawer(typeof(SerializableEvent))]
    public class SerializableEventPropertyDrawer : BasePropertyDrawer<SerializableEvent> {

        protected override void Update() {

            var source = GetValue(field => field.Source);

            if (source != null) {

                using (Property()) {

                    Label();

                    var components = GetComponentDictionary(source);

                    var result = UI.Dropdown(components, field => field.ComponentName);

                    if (result.Index >= 0) {
                        var value = GetValue(field => field.ComponentName);

                        if (value.Contains("|")) {
                            var index = value.Split('|').Last();
                            SetProperty(field => field.Index, index);
                        }
                    }

                    // New scope
                    {
                        var typeName = GetValue(field => field.ComponentName);
                        var index = GetValue(field => field.Index);
                        var component = SerializableComponent.Get(source, typeName, index);
                        var componentType = component.GetType();

                        var dictionary = componentType.GetEvents().ToDictionary(each => each.Name, each => each.Name);

                        LeftGap();
                        UI.Dropdown(dictionary, property => property.EventName);
                    }
                }
            }
        }
    }
}