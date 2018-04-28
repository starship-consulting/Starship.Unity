using System;
using System.Linq;
using System.Reflection;
using Assets.Scripts.Core;
using UnityEditor;

namespace Assets.Scripts.Editor.PropertyDrawers {

    [CustomPropertyDrawer(typeof(SerializableObjectMethod))]
    public class SerializableObjectMethodPropertyDrawer : BasePropertyDrawer<SerializableObjectMethod> {

        protected override void Update() {

            using (Property()) {

                Label();

                Position.x = 60;
                Default(field => field.Behaviour, 120);

                var target = GetValue(field => field.Behaviour);

                if (target != null) {
                    Left(120);

                    var methods = target.GetType().GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);

                    var dictionary = methods.ToDictionary(each => each.Name, each => each.Name);

                    UI.Dropdown(dictionary, property => property.Method);
                }
            }
        }
    }
}