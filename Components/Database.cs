using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core;
using Assets.Scripts.Entities;
using Assets.Scripts.Enumerations;

namespace Assets.Scripts.Components {
    public static class Database {
        static Database() {
            Stores = new Dictionary<Type, DataStore>();
        }

        public static void With<T>(Action<T> action) {
            var entity = Find<T>();

            if (entity != null) {
                action(entity);
            }
        }

        public static void With<T>(Func<T, bool> criteria, Action<T> action) {
            foreach (var entity in Get<T>().Where(criteria)) {
                action(entity);
            }
        }

        public static T Find<T>(Func<T, bool> criteria = null) {
            var query = Get<T>();

            if (criteria != null) {
                query = query.Where(criteria);
            }

            return query.FirstOrDefault();
        }

        /*public static void With<T>(Action<T> callback) where T : Entity {
            New(callback);

            foreach (var entity in Get<T>()) {
                callback(entity);
            }
        }*/
        
        public static IEnumerable<T> Get<T>() {
            return GetStore(typeof(T)).Get().Cast<T>();
        }

        public static IEnumerable Get(Type type) {
            return GetStore(type).Get();
        }

        public static T New<T>() where T : Entity, new() {
            return Add(new T());
        }

        public static Entity New(Type type) {
            return Add((Entity) Activator.CreateInstance(type));
        }

        public static T Add<T>(T entity) {
            var type = entity.GetType();
            return GetStore(type).Add(entity);
        }

        public static T GetOrAdd<T>() {
            return GetOrAdd(Activator.CreateInstance<T>());
        }

        public static T GetOrAdd<T>(T entity) {
            var existing = Find<T>();

            if (existing != null) {
                return existing;
            }

            var type = entity.GetType();
            return GetStore(type).Add(entity);
        }

        public static void Delete<T>(T entity) {
            GetStore(entity.GetType()).Delete(entity);
        }

        public static EventToken OnNew<T>(Action<T> callback) {
            return GetStore(typeof(T)).On(CRUD.Create, typeof(T), callback);
        }

        public static EventToken OnNew(Type type, Action<object> callback) {
            return GetStore(type).On(CRUD.Create, type, callback);
        }

        public static void OnNew(Action<Entity> callback) {
            foreach (var store in Stores) {
                store.Value.On(CRUD.Create, store.Key, callback);
            }
        }

        public static EventToken OnUpdate<T>(Action<T> callback) where T : Entity {
            return GetStore(typeof(T)).On(CRUD.Update, typeof(T), callback);
        }

        public static EventToken OnUpdate(Type type, Action<object> callback) {
            return GetStore(type).On(CRUD.Update, type, callback);
        }

        public static EventToken OnDelete<T>(Action<T> callback) where T : Entity {
            return GetStore(typeof(T)).On(CRUD.Delete, typeof(T), callback);
        }

        public static EventToken OnDelete(Type type, Action<object> callback) {
            return GetStore(type).On(CRUD.Delete, type, callback);
        }

        public static void Off(EventToken token) {
            GetStore(token.EventType).Off(token);
        }

        private static DataStore GetStore(Type type) {
            if (!Stores.ContainsKey(type)) {
                Stores.Add(type, new DataStore());
            }

            return Stores[type];
        }

        private static Dictionary<Type, DataStore> Stores { get; set; }
    }
}