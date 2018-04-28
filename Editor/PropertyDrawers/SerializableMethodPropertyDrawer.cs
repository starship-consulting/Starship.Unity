using System;
using System.Linq;
using System.Reflection;
using Assets.Scripts.Core;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editor.PropertyDrawers {

    [CustomPropertyDrawer(typeof(SerializableMethod))]
    public class SerializableMethodPropertyDrawer : BasePropertyDrawer<SerializableMethod> {

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return 40;
        }

        protected override void Update() {

            using (Property()) {
                Label();
                Default(field => field.Type);
            }

            using (Property()) {
                Newline();
                var target = Type.GetType(GetValue(field => field.Type.Name));

                if (target != null) {
                    var methods = target.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
                    var dictionary = methods.ToDictionary(each => each.Name, each => each.Name);
                    UI.Dropdown(dictionary, property => property.MethodName);
                }
            }

            /*using (Property()) {

                Label();

                Position.x = 60;
                Default(field => field.Target, 120);

                var target = GetValue(field => field.Target);

                if (target != null) {
                    Left(120);

                    var methods = target.GetType().GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);

                    var dictionary = methods.ToDictionary(each => each.Name, each => each.Name);

                    Dropdown(dictionary, property => property.MethodName);
                }
            }*/
        }
    }
}