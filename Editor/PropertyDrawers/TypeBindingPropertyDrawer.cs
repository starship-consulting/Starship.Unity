using System;
using System.Collections.Generic;
using System.Linq;
using Starship.Unity.Models;
using UnityEditor;

namespace Starship.Unity.Editor.PropertyDrawers {

    [CustomPropertyDrawer(typeof(TypeBinding))]
    public class TypeBindingPropertyDrawer : BasePropertyDrawer<TypeBinding> {

        static TypeBindingPropertyDrawer() {

            var items = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("(Inherit)", string.Empty)};

            items.AddRange(typeof (TypeBinding).Assembly.GetTypes()
                .OrderBy(each => each.Name)
                .Select(each => new KeyValuePair<string, string>(each.Name, each.AssemblyQualifiedName)));

            Types = items.ToDictionary(each => each.Key, each => each.Value);
        }

        protected override void Update() {
            using (Property()) {
                Label();
                
                var result = UI.Dropdown(Types, property => property.TypeName);

                if (result.Index > 0) {
                    var type = Type.GetType(GetValue(field => field.TypeName));

                    var fields = type.GetFields()
                        .OrderBy(each => each.Name)
                        .ToDictionary(each => each.Name, each => each.Name);

                    UI.Dropdown(fields, property => property.FieldName);
                }
                else {
                    SetProperty(property => property.FieldName, string.Empty);
                }
            }
        }

        private static Dictionary<string, string> Types { get; set; }
    }
}
