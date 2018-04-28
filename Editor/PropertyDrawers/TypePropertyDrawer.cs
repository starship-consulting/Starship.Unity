using System;
using Assets.Scripts.Core;
using UnityEditor;

namespace Assets.Scripts.Editor.PropertyDrawers {

    [CustomPropertyDrawer(typeof(SerializableType))]
    public class SerializableTypePropertyDrawer : BasePropertyDrawer<SerializableType> {
        protected override void Update() {
            using (Property()) {
                Label();
                UI.TypeDropdown(field => field.Name);
            }
        }
    }

    /*public class MyTypeEditor : BaseFieldEditor<Type> {
        protected override void Draw(EditorState<Type> state) {
            state.Dropdown(state.GetTypes(), field => field.AssemblyQualifiedName, name => Type.GetType(name));
        }
    }*/
}