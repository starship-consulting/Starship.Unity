using Starship.Unity.Core;
using UnityEditor;

namespace Starship.Unity.Editor.PropertyDrawers {

    [CustomPropertyDrawer(typeof(SerializableReference))]
    public class SerializableReferencePropertyDrawer : BasePropertyDrawer<SerializableReference> {

        protected override void Update() {

            using (Property()) {

                Label(SerializedProperty.name);
                DefaultObject(field => field.Type);

                var obj = GetValue(field => field.Type);
                
                if (obj == null) {
                    return;
                }

                var script = obj as MonoScript;

                if (script != null) {
                    
                }

                Newline();
                Default(field => field.Member);

                var member = GetValue(field => field.Member);

                if (member != null) {
                    member.Source = obj;
                }
            }
        }
    }
}