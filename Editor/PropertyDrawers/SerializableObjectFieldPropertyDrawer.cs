using System;
using System.Linq;
using System.Reflection;
using Assets.Scripts.Core;
using UnityEditor;

namespace Assets.Scripts.Editor.PropertyDrawers {

    [CustomPropertyDrawer(typeof(SerializableObjectField))]
    public class SerializableObjectFieldPropertyDrawer : BasePropertyDrawer<SerializableObjectField> {

        protected override void Update() {

            var source = GetValue(field => field.Source);

            if (source == null) {
                return;
            }

            using (Property()) {

                Label();

                var componentType = source.GetType();

                var properties = componentType.GetProperties()
                    .Where(each => each.GetGetMethod() != null)
                    .OrderBy(each => each.Name)
                    .Cast<MemberInfo>();

                var fields = componentType.GetFields()
                    .OrderBy(each => each.Name)
                    .Cast<MemberInfo>();

                var dictionary = fields.Concat(properties).ToDictionary(each => each.Name, each => each.Name);

                UI.Dropdown(dictionary, property => property.PropertyName);
            }
        }
    }
}