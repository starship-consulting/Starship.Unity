namespace Starship.Unity.Editor.PropertyDrawers {

    /*[CustomPropertyDrawer(typeof(SerializableObjectMethod))]
    public class SerializableObjectMethodEditor : BasePropertyDrawer<SerializableObjectMethod> {

        protected override void Update() {
        }

        protected override void Render() {
            serializedObject.Update();

            var target = GetValue(field => field.Target);

            if (target != null) {
                var method = GetValue(field => field.Method);

                if (method.Type == null) {
                    method.Type = new SerializableType(target.GetType());
                }
                else {
                    method.Type.Value = target.GetType();
                }

                //var methods = target.GetType().GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);

                //var dictionary = methods.ToDictionary(each => each.Name, each => each.Name);

                //Dropdown(dictionary, property => property.MethodName);
            }

            serializedObject.ApplyModifiedProperties();

            DrawDefaultInspector();
        }
    }*/
}