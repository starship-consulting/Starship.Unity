using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Starship.Unity.Extensions;
using UnityEditor;
using UnityEngine;

namespace Starship.Unity.Editor.Extensions {
    public static class SerializedPropertyExtensions {
        public static IEnumerable<SerializedProperty> ForEach(this SerializedProperty property) {
            for (var index = 0; index < property.arraySize; index++) {
                yield return property.GetArrayElementAtIndex(index);
            }
        }

        public static T GetValue<T>(this SerializedProperty property) {
            Type valueType = typeof(T);

            if (valueType.IsEnum)
                return (T) Enum.ToObject(valueType, property.enumValueIndex);
            if (typeof(Color).IsAssignableFrom(valueType))
                return (T) (object) property.colorValue;
            if (typeof(LayerMask).IsAssignableFrom(valueType))
                return (T) (object) property.intValue;
            if (typeof(Vector2).IsAssignableFrom(valueType))
                return (T) (object) property.vector2Value;
            if (typeof(Vector3).IsAssignableFrom(valueType))
                return (T) (object) property.vector3Value;
            if (typeof(Rect).IsAssignableFrom(valueType))
                return (T) (object) property.rectValue;
            if (typeof(AnimationCurve).IsAssignableFrom(valueType))
                return (T) (object) property.animationCurveValue;
            if (typeof(Bounds).IsAssignableFrom(valueType))
                return (T) (object) property.boundsValue;
            if (typeof(Gradient).IsAssignableFrom(valueType))
                return (T) (object) SafeGradientValue(property);
            if (typeof(Quaternion).IsAssignableFrom(valueType))
                return (T) (object) property.quaternionValue;
            if (typeof(UnityEngine.Object).IsAssignableFrom(valueType))
                return (T) (object) property.objectReferenceValue;
            if (typeof(int).IsAssignableFrom(valueType))
                return (T) (object) property.intValue;
            if (typeof(bool).IsAssignableFrom(valueType))
                return (T) (object) property.boolValue;
            if (typeof(float).IsAssignableFrom(valueType))
                return (T) (object) property.floatValue;
            if (typeof(string).IsAssignableFrom(valueType))
                return (T) (object) property.stringValue;
            if (typeof(char).IsAssignableFrom(valueType))
                return (T) (object) property.intValue;

            var path = property.propertyPath.Replace(".Array.data[", "[");
            object obj = property.serializedObject.targetObject;
            var elements = path.Split('.');

            foreach (var element in elements) {
                if (element.Contains("[")) {
                    var elementName = element.Substring(0, element.IndexOf("["));
                    var index = Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                    obj = InternalGetValue(obj, elementName, index);
                }
                else {
                    obj = InternalGetValue(obj, element);
                }
            }

            return (T) obj;

            return default(T); //(T) (object) property.objectReferenceValue;
        }

        private static object InternalGetValue(object source, string name) {
            if (source == null)
                return null;
            var type = source.GetType();

            while (type != null) {
                var f = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (f != null)
                    return f.GetValue(source);

                var p = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (p != null)
                    return p.GetValue(source, null);

                type = type.BaseType;
            }

            return null;
        }

        private static object InternalGetValue(object source, string name, int index) {
            var enumerable = InternalGetValue(source, name) as System.Collections.IEnumerable;
            if (enumerable == null) return null;
            var enm = enumerable.GetEnumerator();
            //while (index-- >= 0)
            //    enm.MoveNext();
            //return enm.Current;

            for (int i = 0; i <= index; i++) {
                if (!enm.MoveNext()) return null;
            }

            return enm.Current;
        }

        private static Gradient SafeGradientValue(SerializedProperty sp) {
            BindingFlags instanceAnyPrivacyBindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            PropertyInfo propertyInfo = typeof(SerializedProperty).GetProperty(
                "gradientValue",
                instanceAnyPrivacyBindingFlags,
                null,
                typeof(Gradient),
                new Type[0],
                null
            );
            if (propertyInfo == null)
                return null;

            Gradient gradientValue = propertyInfo.GetValue(sp, null) as Gradient;
            return gradientValue;
        }

        public static SerializedProperty GetProperty<T>(this SerializedProperty property, Expression<Func<T, object>> field) {
            return property.FindPropertyRelative(field.GetFullName());
        }

        public static SerializedProperty GetProperty<T, V>(this SerializedProperty property, Expression<Func<T, V>> field) {
            return property.FindPropertyRelative(field.GetFullName());
        }

        /*public static int Dropdown<T>(this SerializedProperty serializedProperty, Dictionary<string, string> data, Expression<Func<T, object>> field, int width = 115) {

            if (!data.Any()) {
                data.Add(string.Empty, string.Empty);
            }

            if (width > 0) {
                //Position.width = width;
            }

            var labels = data.Keys.ToList();
            var values = data.Values.ToList();

            var selectedItem = values.IndexOf(serializedProperty.stringValue);

            if (selectedItem < 0) {
                selectedItem = 0;
            }

            var value = EditorGUI.Popup(selectedItem, labels.ToArray());

            if (value < 0) {
                value = 0;
            }

            serializedProperty.stringValue = values[value];

            //Left(100);

            return value;
        }*/
    }
}