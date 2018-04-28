using System;
using Assets.Scripts.Databinding;
using Assets.Scripts.Extensions;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editor.Editors {

    [CustomEditor(typeof(Binding))]
    public class BindingEditor : BaseEditor<Binding> {

        protected override void Render(Binding model) {
            EditorGUILayout.PropertyField(GetProperty(each => each.Source));
            EditorGUILayout.PropertyField(GetProperty(each => each.Target));

            //RenderTarget();

            /*var bindedModel = GetProperty(each => each.BindedModel);
            EditorGUILayout.PropertyField(bindedModel, true);
            
            var isBinded = GetProperty(each => each.IsBinded);
            EditorGUILayout.PropertyField(isBinded, true);*/
        }

        private void RenderTarget() {
            var isPrefab = false;

            var target = GetProperty(each => each.Target);

            var binding = target.serializedObject.targetObject as Binding;

            if (binding.Target != null && (GameObject)binding.Target.Source != binding.gameObject) {
                isPrefab = serializedObject.FindProperty("TargetIsPrefab").boolValue = binding.Target.Source.As<GameObject>().IsPrefab();
            }

            EditorGUILayout.ObjectField(target);
            //EditorGUILayout.PropertyField(target, true);

            if (isPrefab) {
                //RenderPrefab();
            }
            else {
                //RenderField(binding);
            }
        }

        private void RenderField(Binding binding) {
            /*if (binding.Target != null) {
                var field = GetProperty(each => each.Field);
                field.FindPropertyRelative("Source").objectReferenceValue = (GameObject)binding.Target.Type;

                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(field, true);
                EditorGUI.indentLevel--;
            }*/
        }

        private void RenderPrefab() {
            var template = GetProperty(each => each.Template);
            EditorGUILayout.PropertyField(template, true);
        }
    }
}