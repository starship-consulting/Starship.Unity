using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Assets.Scripts.Editor.Extensions;
using Assets.Scripts.Editor.Helpers;
using Assets.Scripts.Editor.Interfaces;
using Assets.Scripts.Extensions;
using Assets.Scripts.Utilities;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editor.PropertyDrawers {
    public abstract class BasePropertyDrawer<T> : PropertyDrawer, IsEditor<T> where T : class {
        
        public T GetModel() {
            var target = SerializedProperty.serializedObject.targetObject;
            var value = target.ReadField<T>(SerializedProperty.name);
            return value;
        }

        public Dictionary<string, string> GetComponentDictionary(GameObject source) {
            var components = new Dictionary<string, string>();

            foreach (var component in source.GetComponents<MonoBehaviour>()) {
                var name = component.GetType().Name;

                var index = 1;

                while (true) {
                    var test = name;

                    if (index > 1) {
                        test = name + " " + index;
                    }

                    if (!components.ContainsKey(test)) {
                        components.Add(test, component.GetType().AssemblyQualifiedName + "|" + (index - 1));
                        break;
                    }

                    index += 1;
                }
            }

            return components;
        }

        public sealed override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            Height = 0;
            Position = position;
            SerializedProperty = property;
            Content = label;

            if (UI == null) {
                UI = new UIHelper<T>(this);
            }

            Update();
        }

        protected abstract void Update();

        public void Indent() {
            EditorGUI.indentLevel += 1;
        }

        public void Right(int pixels) {
            Position.x -= pixels;
        }

        public void Left(int pixels) {
            Position.x += pixels;
        }

        public void Newline() {
            Height += 20;
            Position.y += 20;
        }

        public void LeftGap() {
            Left(17);
        }

        public void Next() {
            LeftGap();
            Left(100);
        }
        
        protected void DefaultObject(Expression<Func<T, object>> field, int width = 0) {
            if (width > 0) {
                Position.width = width;
            }

            EditorGUI.ObjectField(Position, GetProperty(field), GUIContent.none);
        }

        protected R Default<R>(Expression<Func<T, R>> field, int width = 0) where R : class {
            if (width > 0) {
                Position.width = width;
            }

            var property = GetProperty(field);
            EditorGUI.PropertyField(Position, property, GUIContent.none, false);
            return GetModel().ReadField<R>(field.MemberName());
        }

        protected Disposer Property(SerializedProperty property = null) {
            if (property == null) {
                property = SerializedProperty;
            }

            return new Disposer(() => EditorGUI.BeginProperty(Position, Content, property), EditorGUI.EndProperty);
        }

        protected void Label(string text = "") {
            UI.Label(text);
        }
        
        protected void SetProperty(Expression<Func<T, object>> field, string value) {
            GetProperty(field).stringValue = value;
        }

        public R GetValue<R>(Expression<Func<T, R>> field) where R : class {
            return GetProperty(field).GetValue<R>();
        }
        
        public SerializedProperty GetProperty(Expression<Func<T, object>> field) {
            return SerializedProperty.GetProperty(field);
        }

        public SerializedProperty GetProperty<V>(Expression<Func<T, V>> field) {
            return SerializedProperty.GetProperty(field);
        }

        public MonoBehaviour GetBehaviour() {
            return SerializedProperty.serializedObject.targetObject as MonoBehaviour;
        }

        public List<MonoBehaviour> GetComponents() {
            var behaviour = GetBehaviour();

            if (behaviour != null) {
                return behaviour.GetComponents<MonoBehaviour>().ToList();
            }

            return new List<MonoBehaviour>();
        }

        public UIHelper<T> UI;

        public Rect Position;

        public GUIContent Content;

        public SerializedProperty SerializedProperty;

        public int Height { get; set; }
    }
}