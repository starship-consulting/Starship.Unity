using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Assets.Scripts.Editor.Extensions;
using UnityEditor;

namespace Assets.Scripts.Editor.Interfaces {
    public interface IsEditor<T> where T : class {
        SerializedProperty GetProperty(Expression<Func<T, object>> field);
    }

    public static class IsEditorExtensions {

        public static IEnumerable<SerializedProperty> ForEach<T>(this IsEditor<T> editor, Expression<Func<T, object>> field) where T : class {
            return editor.GetProperty(field).ForEach();
        }

        public static SerializedProperty NewArrayItem<T>(this IsEditor<T> editor, Expression<Func<T, object>> field) where T : class {
            var property = editor.GetProperty(field);
            property.InsertArrayElementAtIndex(0);
            return property.GetArrayElementAtIndex(0);
        }
    }
}