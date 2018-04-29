using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Starship.Unity.Extensions;
using Starship.Unity.Utilities;
using UnityEditor;
using UnityEngine;

namespace Starship.Unity.Editor.Editors {
    public abstract class BaseCustomEditor<T> : ICustomEditor {

        public void Initialize(T model, SerializedProperty property = null) {
            Model = model;
            Property = property;

            Draw(model);
        }

        public void DrawProperty<V>(Expression<Func<T, V>> expression) {
            var field = expression.GetMember() as FieldInfo;

            if (field == null) {
                throw new Exception("Cannot draw invalid field: " + expression);
            }

            var serializedProperty = Property.FindPropertyRelative(expression.GetFullName());
            DrawField(field, serializedProperty);
        }

        public void DrawChildren() {

            var fields = Model.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);

            if (fields.Any()) {
                foreach (var field in fields) {
                    var property = Property.FindPropertyRelative(field.Name);
                    DrawField(field, property);
                }
            }
        }

        protected SerializedProperty GetProperty(Expression<Func<T, object>> field) {
            return Property.FindPropertyRelative(field.GetFullName());
        }

        protected SerializedProperty GetProperty<V>(Expression<Func<T, V>> field) {
            return Property.FindPropertyRelative(field.GetFullName());
        }

        protected int EventDropdown(object source, Expression<Func<T, object>> target) {

            if (source == null) {
                return -1;
            }

            var events = source.GetType()
                .GetEvents()
                .ToDictionary(key => key.Name, value => value.Name);

            return Dropdown(events, target);
        }

        protected int MethodDropdown(object source, Expression<Func<T, object>> target) {

            var methods = source.GetType()
                .GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(key => key.Name, value => value.Name);

            return Dropdown(methods, target);
        }

        protected int BehaviourDropdown(GameObject source, Expression<Func<T, MonoBehaviour>> target) {

            var components = source.GetComponents<MonoBehaviour>().ToList();

            if (!components.Any()) {
                return -1;
            }

            var label = target.MemberName();
            var component = target.Compile().Invoke(Model);
            var selectedComponent = components.IndexOf(component);

            if (selectedComponent < 0) {
                selectedComponent = 0;
            }

            var items = components.Select(each => new GUIContent(each.GetType().Name)).ToArray();
            selectedComponent = EditorGUILayout.Popup(new GUIContent(label), selectedComponent, items);

            if (selectedComponent < 0) {
                selectedComponent = 0;
            }

            GetProperty(target).objectReferenceValue = components[selectedComponent];

            return selectedComponent;
        }

        protected int Dropdown(Dictionary<string, string> data, Expression<Func<T, object>> field) {
            return Dropdown(data, GetProperty(field));
        }

        protected int Dropdown(Dictionary<string, string> data, SerializedProperty property) {

            if (!data.Any()) {
                data.Add(string.Empty, string.Empty);
            }

            var labels = data.Keys.ToArray();
            var values = data.Values.ToList();

            var selectedItem = values.IndexOf(property.stringValue);

            if (selectedItem < 0) {
                selectedItem = 0;
            }

            var value = EditorGUILayout.Popup(new GUIContent(property.name), selectedItem, labels.Select(each => new GUIContent(each)).ToArray());

            if (value < 0) {
                value = 0;
            }

            property.stringValue = values[value];

            return value;
        }

        private void DrawField(FieldInfo field, SerializedProperty property) {

            if (property == null) {
                return;
            }

            try {
                var editor = EditorFactory.GetEditorForType(field.FieldType);

                if (editor != null) {

                    editor.GetType()
                        .GetMethod("Initialize")
                        .Invoke(editor, new[] {field.GetValue(Model), property});
                }
                else {
                    EditorGUILayout.PropertyField(property);
                }
            }
            catch (ArgumentException) {
            }
            catch (ExitGUIException) {
            }
            catch (Exception ex) {
                throw new Exception("Failed to draw '" + field.Name + "': " + ex);
            }
        }

        public abstract void Draw(T model);

        public void Label(string text = "") {
            if (string.IsNullOrEmpty(text)) {
                text = Property.name;
            }

            EditorGUILayout.LabelField(text);
        }

        public Disposer Indent() {
            return new Disposer(() => {
                EditorGUI.indentLevel++;
            }, () => {
                EditorGUI.indentLevel--;
            });
        }

        public SerializedProperty Property { get; set; }

        public T Model { get; set; }
    }
}