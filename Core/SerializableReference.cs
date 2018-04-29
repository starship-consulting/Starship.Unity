using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Starship.Unity.Core {

    [Serializable]
    public class SerializableReference {

        /*public SerializableReference() {
            Member = new SerializableMember();
        }*/

        [HideInInspector]
        public Object Type;

        [HideInInspector]
        public SerializableMember Member;
    }
}