using System;

namespace Assets.Scripts.Tasks {

    [Serializable]
    public class TaskInstance {
        
        //[ValidTypes(typeof(Task))]
        //public SerializableType Type;

        public PlayAnimationTask AnimationTask;

        public Task Task;
        
        /*public override string ToString() {
            var type = Type.GetSerializedType();

            if (type != null) {
                return type.Name.Replace("Task", "").SplitCamelCase();
            }

            return base.ToString();
        }*/
    }
}