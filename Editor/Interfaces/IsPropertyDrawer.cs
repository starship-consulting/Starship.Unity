using System;
using System.Linq.Expressions;
using UnityEditor;

namespace Starship.Unity.Editor.Interfaces {
    public interface IsPropertyDrawer<T> {
    }

    public static class IsPropertyDrawerExtensions {

        public static SerializedProperty GetProperty<T>(this IsPropertyDrawer<T> drawer, Expression<Func<T, object>> field) {
            return null;
            //return drawer.SerializedProperty.FindPropertyRelative(field.GetFullName());
        }

        /*public static Dictionary<string, string> GetTypes(this IsPropertyDrawer<T> drawer) {
            var attribute = fieldInfo.GetCustomAttributes(typeof(ValidTypesAttribute), true).FirstOrDefault() as ValidTypesAttribute;
            var validTypes = ValidTypes;

            if (attribute != null) {
                validTypes = attribute.Types.ToList();
            }

            var types = typeof(TypeBinding).Assembly.GetTypes().Where(each => !each.IsAbstract && !each.ContainsGenericParameters && validTypes.Any(valid => valid.IsAssignableFrom(each)));

            return types.OrderBy(each => each.Name).ToDictionary(each => each.Name, each => each.AssemblyQualifiedName);
        }*/

        /*public static int Dropdown<T>(this IsPropertyDrawer<T> drawer, Dictionary<string, string> data, Expression<Func<T, object>> field, int width = 115) {
            if (!data.Any()) {
                data.Add(string.Empty, string.Empty);
            }

            if (width > 0) {
                //drawer.Position.width = width;
            }

            var labels = data.Keys.ToList();
            var values = data.Values.ToList();

            var property = drawer.GetProperty(field);

            var selectedItem = values.IndexOf(property.stringValue);

            if (selectedItem < 0) {
                selectedItem = 0;
            }

            var value = EditorGUI.Popup(drawer.GetPosition(), selectedItem, labels.ToArray());

            if (value < 0) {
                value = 0;
            }

            property.stringValue = values[value];

            //Left(100);

            return value;
        }*/
    }
}