using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Starship.Unity.Attributes;
using Starship.Unity.Core;
using Starship.Unity.Editor.Data;
using Starship.Unity.Editor.Extensions;
using Starship.Unity.Editor.PropertyDrawers;
using Starship.Unity.Models;
using UnityEditor;
using UnityEngine;

namespace Starship.Unity.Editor.Helpers {
    public class UIHelper<T> where T : class {
        public UIHelper(BasePropertyDrawer<T> drawer) {
            Drawer = drawer;
        }

        public R PropertyField<R>(Expression<Func<T, R>> field) where R : class {
            var property = Drawer.GetProperty(field);
            EditorGUILayout.PropertyField(property, GUIContent.none, false);
            return property.GetValue<R>();
        }

        public void PropertyField(SerializedProperty property) {
            EditorGUILayout.PropertyField(property, GUIContent.none, false);
        }

        public Dictionary<string, string> GetTypeDictionary() {
            var attribute = Drawer.fieldInfo.GetCustomAttributes(typeof(ValidTypesAttribute), true).FirstOrDefault() as ValidTypesAttribute;
            var validTypes = ValidTypes;

            if (attribute != null) {
                validTypes = attribute.Types.ToList();
            }

            var types = typeof(TypeBinding).Assembly.GetTypes()
                .Where(each => !each.IsAbstract && !each.ContainsGenericParameters && validTypes.Any(valid => valid.IsAssignableFrom(each)))
                .OrderBy(each => each.Name)
                .ToArray();

            return ConvertToDictionary(types);
        }

        private static Dictionary<string, string> ConvertToDictionary(params Type[] types) {
            return types.ToDictionary(each => each.Name, each => each.AssemblyQualifiedName);
        }

        public void Label(string text = "") {
            if (string.IsNullOrEmpty(text)) {
                text = Drawer.Content.text;
            }

            EditorGUILayout.PrefixLabel(text);
        }

        public void TypeDropdown(Expression<Func<T, object>> field) {
            Dropdown(GetTypeDictionary(), field);
        }

        public int IntField(Expression<Func<T, int>> field) {
            var property = Drawer.GetProperty(field);
            property.intValue = EditorGUILayout.IntField(property.intValue);
            return property.intValue;
        }

        public string TextField(Expression<Func<T, object>> field) {
            var property = Drawer.GetProperty(field);
            var value = property.stringValue = EditorGUILayout.TextField(property.stringValue);
            return value;
        }

        public Type TypeDropdown(Expression<Func<T, object>> field, params Type[] types) {
            var result = Dropdown(ConvertToDictionary(types), field);
            return types.FirstOrDefault(each => each.AssemblyQualifiedName == result.Value);
        }

        public SelectedDropdownItem Dropdown(Dictionary<string, string> data, Expression<Func<T, object>> field) {
            if (!data.Any()) {
                data.Add(string.Empty, string.Empty);
            }

            var labels = data.Keys.ToList();
            var values = data.Values.ToList();

            var property = Drawer.GetProperty(field);
            var oldValue = property.stringValue;

            var selectedItem = values.IndexOf(property.stringValue);

            if (selectedItem < 0) {
                selectedItem = 0;
            }

            var value = EditorGUILayout.Popup(selectedItem, labels.ToArray());

            if (value < 0) {
                value = 0;
            }

            property.stringValue = values[value];

            if (oldValue != property.stringValue) {
                Drawer.SerializedProperty.serializedObject.ApplyModifiedProperties();
            }

            return new SelectedDropdownItem {
                Index = value,
                Key = labels[value],
                Value = values[value]
            };
        }

        private BasePropertyDrawer<T> Drawer { get; set; }

        private static readonly List<Type> ValidTypes = new List<Type> {
            typeof(BaseComponent)
        };
    }
}