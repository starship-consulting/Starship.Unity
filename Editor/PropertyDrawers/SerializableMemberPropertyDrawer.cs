using System;
using System.Linq;
using System.Reflection;
using Assets.Scripts.Core;
using Assets.Scripts.Extensions;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editor.PropertyDrawers {

    [CustomPropertyDrawer(typeof(SerializableMember))]
    public class SerializableMemberPropertyDrawer : BasePropertyDrawer<SerializableMember> {

        protected override void Update() {
            using (Property()) {
                Label();
                Default(field => field.Source);
                Newline();

                var source = GetValue(field => field.Source);

                if (source == null) {
                    return;
                }

                var componentType = source.GetType();
                var bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly;

                if (componentType.Is<MonoScript>()) {
                    componentType = source.As<MonoScript>().GetClass();
                }

                if (componentType.Is<MonoBehaviour>() || componentType.Is<ScriptableObject>()) {

                    var properties = componentType.GetProperties(bindingFlags)
                        .Where(each => each.GetGetMethod() != null)
                        .OrderBy(each => each.Name)
                        .Cast<MemberInfo>();

                    var fields = componentType.GetFields(bindingFlags)
                        .OrderBy(each => each.Name)
                        .Cast<MemberInfo>();

                    var dictionary = fields.Concat(properties).Concat(componentType.GetEvents(bindingFlags)).ToDictionary(each => each.Name, each => each.Name);

                    EditorGUILayout.Space();
                    EditorGUILayout.Space();
                    EditorGUILayout.Space();

                    UI.Dropdown(dictionary, property => property.MemberName);
                }
                else {
                    var gameObject = source as GameObject;

                    if (gameObject.IsPrefab()) {
                        return;
                    }

                    var components = GetComponentDictionary(gameObject);
                    var result = UI.Dropdown(components, field => field.ComponentName);

                    if (result.Index >= 0) {
                        var value = GetValue(field => field.ComponentName);

                        if (value.Contains("|")) {
                            SetProperty(field => field.Index, value.Split('|').Last());
                        }
                    }
                    
                    var typeName = GetValue(field => field.ComponentName);
                    var index = GetValue(field => field.Index);
                    var component = SerializableComponent.Get(gameObject, typeName, index);
                    var chosenComponentType = component.GetType();

                    var properties = chosenComponentType.GetProperties(bindingFlags)
                        .Where(each => each.GetGetMethod() != null)
                        .OrderBy(each => each.Name)
                        .Cast<MemberInfo>();

                    var fields = chosenComponentType.GetFields(bindingFlags)
                        .OrderBy(each => each.Name)
                        .Cast<MemberInfo>();

                    var componentDictionary = fields.Concat(properties)
                        .Concat(chosenComponentType.GetEvents(bindingFlags))
                        .ToDictionary(each => each.Name, each => each.Name);

                    EditorGUILayout.Space();
                    EditorGUILayout.Space();
                    EditorGUILayout.Space();

                    LeftGap();
                    UI.Dropdown(componentDictionary, property => property.MemberName);
                }
            }
        }
    }
}