using Starship.Unity.Models;
using UnityEditor;

namespace Starship.Unity.Editor.PropertyDrawers {

    [CustomPropertyDrawer(typeof(FieldDefinition))]
    public class FieldDefinitionDrawer : BasePropertyDrawer<FieldDefinition> {

        protected override void Update() {
            using (Property()) {
                Label();

                var types = new [] {
                    typeof(int),
                    typeof(string),
                    typeof(float)
                };

                UI.TextField(field => field.Name);

                var type = UI.TypeDropdown(field => field.Type, types);

                if (type != null) {

                }
            }
        }
    }
}