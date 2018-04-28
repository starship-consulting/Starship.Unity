using System;
using UnityEngine;

namespace Assets.Scripts.Core {

    [Serializable]
    public class SerializableEventInfo {

        public MonoBehaviour Source;

        public string EventName;
    }
}