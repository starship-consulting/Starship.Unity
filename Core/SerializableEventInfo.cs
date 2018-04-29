using System;
using UnityEngine;

namespace Starship.Unity.Core {

    [Serializable]
    public class SerializableEventInfo {

        public MonoBehaviour Source;

        public string EventName;
    }
}