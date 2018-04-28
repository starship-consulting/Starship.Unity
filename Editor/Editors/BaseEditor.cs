using System;
using System.Linq.Expressions;
using Assets.Scripts.Editor.Extensions;
using Assets.Scripts.Editor.Interfaces;
using Assets.Scripts.Extensions;
using Assets.Scripts.Utilities;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editor.Editors {
    public abstract class BaseEditor<T> : UnityEditor.Editor, IsEditor<T> where T : class {
        public BaseEditor() {
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();
            Render(GetModel());
            serializedObject.ApplyModifiedProperties();
        }

        public T GetModel() {
            return serializedObject.targetObject as T;
        }

        public SerializedProperty GetProperty(Expression<Func<T, object>> field) {
            return serializedObject.FindProperty(field.GetFullName());
        }

        protected SerializedProperty GetProperty<V>(Expression<Func<T, V>> field) {
            return serializedObject.FindProperty(field.GetFullName());
        }

        protected R GetValue<R>(Expression<Func<T, R>> field) where R : class {
            return GetProperty(field).GetValue<R>();
        }

        protected abstract void Render(T model);

        protected T Component {
            get { return serializedObject.targetObject as T; }
        }

        protected void Label(string text = "") {
            EditorGUILayout.LabelField(text);
        }

        protected UnityEngine.Object ObjectField(string label, UnityEngine.Object value, Type type) {
            return EditorGUILayout.ObjectField(label, value, type, true);
        }

        protected void PropertyField(SerializedProperty property) {
            EditorGUILayout.PropertyField(property);
        }

        protected void PropertyField(SerializedProperty property, string label) {
            EditorGUILayout.PropertyField(property, new GUIContent(label));
        }

        protected R PropertyField<R>(SerializedProperty property) {
            EditorGUILayout.PropertyField(property);
            return property.GetValue<R>();
        }

        protected R PropertyField<R>(Expression<Func<T, R>> expression) {
            var property = GetProperty(expression);
            EditorGUILayout.PropertyField(property);
            return property.GetValue<R>();
        }

        protected Disposer Indent() {
            return new Disposer(() => { EditorGUI.indentLevel++; }, () => { EditorGUI.indentLevel--; });
        }
    }
}