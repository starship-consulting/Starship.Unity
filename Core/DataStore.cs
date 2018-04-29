using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Starship.Unity.Enumerations;

namespace Starship.Unity.Core {
    public class DataStore {

        public DataStore() {
            Entities = new Dictionary<object, object>();
            EventContainers = new Dictionary<CRUD, EventContainer>();

            foreach (CRUD item in Enum.GetValues(typeof(CRUD))) {
                EventContainers.Add(item, new EventContainer());
            }
        }

        public IEnumerable Get() {
            return Entities.Keys.ToList();
        }

        public void Delete(object entity) {
            if (Entities.ContainsKey(entity)) {
                Entities.Remove(entity);
                EventContainers[CRUD.Delete].Invoke(entity);
            }
        }

        public T Add<T>(T entity) {
            if (!Entities.ContainsKey(entity)) {
                Entities.Add(entity, entity);
                EventContainers[CRUD.Create].Invoke(entity);
            }

            return entity;
        }

        internal void Off(EventToken token) {
            EventContainers[(CRUD)token.EventPartition].Off(token);
        }

        public EventToken On(CRUD eventType, Type entityType, MulticastDelegate callback) {
            var token = new EventToken(eventType, entityType);
            EventContainers[eventType].On(token, callback);
            return token;
        }

        public void Invoke(CRUD eventType, object entity) {
            EventContainers[eventType].Invoke(entity);
        }

        private Dictionary<CRUD, EventContainer> EventContainers { get; set; }

        private Dictionary<object, object> Entities { get; set; }

        private int Index { get; set; }
    }
}
