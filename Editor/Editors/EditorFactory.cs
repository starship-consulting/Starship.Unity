using System;
using System.Collections.Generic;
using System.Linq;
using Starship.Unity.Extensions;

namespace Starship.Unity.Editor.Editors {
    public static class EditorFactory {

        static EditorFactory() {
            Editors = new Dictionary<Type, Type>();

            foreach (var editor in typeof(ICustomEditor).Assembly.GetTypesOf<ICustomEditor>(false)) {
                var method = editor.GetMethod("Draw");
                var type = method.GetParameters().First().ParameterType;

                Editors.Add(type, editor);
            }
        }

        public static ICustomEditor GetEditorForType(Type type) {
            if (Editors.ContainsKey(type)) {
                return (ICustomEditor)Activator.CreateInstance(Editors[type]);
            }

            return null;
        }

        private static Dictionary<Type, Type> Editors { get; set; }
    }
}
