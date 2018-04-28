using System;
using System.Linq;
using Assets.Scripts.Core;
using Assets.Scripts.Models;
using UnityEditor;

namespace Assets.Scripts.Editor.PropertyDrawers {

    [CustomPropertyDrawer(typeof(BehaviourBinding))]
    public class BehaviourBindingPropertyDrawer : BasePropertyDrawer<BehaviourBinding> {

        protected override void Update() {

            using (Property()) {
                Label();
                Default(field => field.Component);

                var behaviour = GetBehaviour();

                var typeName = GetValue(field => field.Component.TypeName);
                var index = GetValue(field => field.Component.Index);
                var component = SerializableComponent.Get(behaviour, typeName, index);

                var fields = component.GetType()
                    .GetProperties()
                    .Where(each => each.GetGetMethod() != null)
                    .OrderBy(each => each.Name)
                    .ToDictionary(each => each.Name, each => each.Name);

                Next();
                UI.Dropdown(fields, property => property.PropertyName);
            }
        }
    }
}
