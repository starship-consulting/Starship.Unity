using Starship.Unity.Core;
using UnityEditor;

namespace Starship.Unity.Editor.Editors {

    public class SerializableObjectEventEditor : BaseCustomEditor<SerializableObjectEvent> {

        public override void Draw(SerializableObjectEvent model) {
            Label();

            using (Indent()) {
                DrawProperty(property => property.Object);

                if (model.Object != null) {
                    using (new EditorGUILayout.HorizontalScope()) {
                        BehaviourDropdown(model.Object, property => property.Behaviour);
                    }

                    EventDropdown(model.Behaviour, target => target.Event);
                }
            }
        }
    }
}