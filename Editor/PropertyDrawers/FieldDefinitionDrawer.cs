using System;
using Assets.Scripts.Models;
using Assets.Scripts.ScriptableObjects;
using UnityEditor;

namespace Assets.Scripts.Editor.PropertyDrawers {

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