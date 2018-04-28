/*using System;
using Assets.Scripts.Utilities;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Assets.Scripts.Extensions {
    public static class SerializedPropertyExtensions {

        public static void AbstractPropertyDrawer(this SerializedProperty property) {
            if (property.propertyType == SerializedPropertyType.ObjectReference) {
                if (property.objectReferenceValue == null) {
                    //field is null, provide object field for user to insert instance to draw
                    EditorGUILayout.PropertyField(property);
                    return;
                }
                Type concreteType = property.objectReferenceValue.GetType();
                UnityEngine.Object castedObject = (UnityEngine.Object) Convert.ChangeType(property.objectReferenceValue, concreteType);

                Editor editor = Editor.CreateEditor(castedObject);

                editor.OnInspectorGUI();
            }
            else {
                //otherwise fallback to normal property field
                EditorGUILayout.PropertyField(property);
            }
        }

        public static Type GetScriptType(this SerializedProperty property) {
            var scriptName = property.ReadProperty<string>("objectReferenceStringValue");
            return TypeCache.Lookup(scriptName);
        }
    }
}*/