using System.Collections.Generic;
using System.Linq;
using Starship.Unity.Core;
using UnityEditor;

namespace Starship.Unity.Editor.PropertyDrawers {

    [CustomPropertyDrawer(typeof(SerializableComponent))]
    public class SerializableComponentPropertyDrawer : BasePropertyDrawer<SerializableComponent> {

        protected override void Update() {
            using (Property()) {
                Label();

                var components = new Dictionary<string, string>();

                foreach (var component in GetComponents()) {
                    var name = component.GetType().Name;

                    var index = 1;

                    while (true) {

                        var test = name;

                        if (index > 1) {
                            test = name + " " + index;
                        }

                        if (!components.ContainsKey(test)) {
                            components.Add(test, component.GetType().AssemblyQualifiedName + "|" + (index-1));
                            break;
                        }

                        index += 1;
                    }
                }
                
                var result = UI.Dropdown(components, field => field.TypeName);

                if (result.Index >= 0) {
                    var value = GetValue(field => field.TypeName);

                    if (value.Contains("|")) {
                        var index = value.Split('|').Last();
                        SetProperty(field => field.Index, index);
                    }
                }
            }
        }
    }
}