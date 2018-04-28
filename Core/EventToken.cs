using System;
using Assets.Scripts.Components;

namespace Assets.Scripts.Core {
    public class EventToken {

        internal EventToken(object partition, Type type) {
            EventPartition = partition;
            EventType = type;
        }

        public void Unregister() {
            Database.Off(this);
        }

        internal object EventPartition { get; set; }

        internal Type EventType { get; set; }
    }
}