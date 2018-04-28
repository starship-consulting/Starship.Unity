using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enumerations;
using Assets.Scripts.Events;
using Assets.Scripts.Events.Models;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Scheduling;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Extensions {
    public static class GameObjectExtensions {

        public static bool HasComponent(this GameObject source, Type componentType) {
            return source.GetComponent(componentType) != null;
        }

        public static bool HasComponent<T>(this GameObject source) {
            return source.GetComponent<T>() != null;
        }

        public static T GetOrAdd<T>(this Behaviour source) where T : Component {
            return source.gameObject.GetOrAdd<T>();
        }

        public static T GetOrAdd<T>(this GameObject source) where T : Component {
            var component = source.GetComponent<T>();

            if (component == null) {
                component = source.AddComponent<T>();
            }

            return component;
        }

        public static T AddComponent<T>(this Behaviour source, Action<T> constructor = null) where T : Component {
            var component = source.gameObject.AddComponent<T>();

            if (constructor != null) {
                constructor(component);
            }

            ExecuteEvents.Execute<IsComponentObserver>(source.gameObject, null, (target, data) => target.OnComponentStateChanged(new ComponentStateChanged(source, ComponentStates.Created)));

            return component;
        }

        public static Promise DeferUntilNextFixedUpdate(this MonoBehaviour source, Action action) {
            var promise = new Promise(action);
            source.StartCoroutine(InternalDeferUntilNextFixedUpdate(promise));
            return promise;
        }

        private static IEnumerator InternalDeferUntilNextFixedUpdate(Promise promise) {
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            promise.Run();
        }

        public static void Routine(this MonoBehaviour source, Action action, TimeSpan delay) {
            source.StartCoroutine(RunRoutine(action, delay));
        }

        private static IEnumerator RunRoutine(Action action, TimeSpan delay) {
            yield return new WaitForSeconds(Convert.ToSingle(delay.TotalSeconds));
            action();

            /*while (task.IsRepeating) {
                yield return new WaitForSeconds(Convert.ToSingle(delay.TotalSeconds));
                action();
            }*/
        }

        public static void CopyComponentsFrom(this GameObject to, GameObject from) {

            to.SetActive(false);

            foreach(var component in from.GetComponents<Behaviour>()) {
                if(to.HasComponent(component.GetType())) {
                    continue;
                }

                var clone = to.AddComponent(component.GetType());
                component.CopyTo(clone);
            }

            to.SetActive(true);
        }

        public static void CopyComponents(this Component source, GameObject target) {
            foreach (var component in source.GetComponents<Behaviour>()) {
                if (component == source || !component.enabled) {
                    continue;
                }

                var clone = target.AddComponent(component.GetType());
                component.CopyTo(clone);
            }
        }

        public static void RemoveAll<T>(this Component source) where T : Component {
            foreach (var component in source.GetComponents<T>()) {
                Destroy(component);
            }
        }

        public static void Remove<T>(this Component source, T component) where T : Component {
            source.gameObject.Remove(component);
        }

        public static void Remove<T>(this Component source) where T : Component {
            source.gameObject.Remove(source.GetComponent<T>());
        }

        public static void Remove<T>(this GameObject source) where T : Component {
            source.Remove(source.GetComponent<T>());
        }

        public static void Remove<T>(this GameObject source, T component) where T : Component {
            if (component != null) {
                Destroy(component);
            }
        }

        public static void With<T>(this Component source, Action<T> action) where T : Component {
            source.gameObject.With<T>(action);
        }

        public static void With<T>(this GameObject source, Action<T> action) where T : Component {
            var component = source.FindComponent<T>();

            if (component != null) {
                action(component);
            }
        }

        public static void Enable<T>(this Component source) where T : MonoBehaviour {
            var component = source.GetComponent<T>();

            if (component != null) {
                component.enabled = true;
            }
        }

        public static void Disable<T>(this Component source) where T : MonoBehaviour {
            var component = source.GetComponent<T>();

            if (component != null) {
                component.enabled = false;
            }
        }

        public static T FindInParents<T>(this Component source) where T : Component {
            return FindInParents<T>(source.gameObject);
        }

        public static T FindInParents<T>(this GameObject source) where T : Component {
            return (T) FindInParents(source, typeof(T));
        }

        public static Component FindInParents(this GameObject source, Type type) {
            if (source == null) {
                return null;
            }

            var component = source.GetComponent(type);

            if (component != null) {
                return component;
            }

            var parent = source.transform.parent;

            if (parent == null) {
                return null;
            }

            return parent.gameObject.FindInParents(type);
        }

        public static T FindInSiblings<T>(this Component source) where T : Behaviour {
            var component = source.GetComponent<T>();

            if (component != null) {
                return component;
            }

            if (source.transform.parent == null) {
                return null;
            }

            foreach (Transform child in source.transform.parent.transform) {
                var childComponent = child.FindComponent<T>();

                if (childComponent != null) {
                    return childComponent;
                }
            }

            return null;
        }

        public static T FindComponent<T>(this Component source) where T : Component {
            return source.gameObject.FindComponent<T>();
        }

        public static T FindComponent<T>(this GameObject source) where T : Component {
            var component = source.GetComponent<T>();

            if(component != null) {
                return component;
            }

            foreach (Transform child in source.transform) {
                var match = child.gameObject.FindComponent<T>();

                if(match != null) {
                    return match;
                }
            }

            return null;
        }

        public static IEnumerable<T> FindComponents<T>(this GameObject source) {
            foreach (var component in source.GetComponents<T>()) {
                yield return component;
            }

            foreach (Transform child in source.transform) {
                foreach (var each in child.gameObject.FindComponents<T>()) {
                    yield return each;
                }
            }
        }

        public static void DeleteParents(this GameObject target) {
            if (target.transform.parent != null) {
                target.transform.parent.gameObject.DeleteParents();
            }
            else {
                target.Delete();
            }
        }

        public static void Delete(this GameObject target) {
            var deletable = target.GetComponent<IsDeletable>();

            if (deletable != null) {
                deletable.Delete();
            }
            else {
                Object.Destroy(target);
            }
        }

        public static void Destroy(this Component behaviour) {
            Object.Destroy(behaviour);
        }

        public static GameObject GetFirstChild(this Component component) {
            foreach(Transform child in component.transform) {
                return child.gameObject;
            }

            return null;
        }

        public static void ClearChildren(this Component behaviour, bool immediate) {
            var match = (from Transform child in behaviour.transform select child.gameObject);
            ClearChildrenInternal(immediate, match);
        }

        public static void ClearChildren(this Component behaviour) {
            behaviour.ClearChildren(Application.isEditor);
        }

        private static void ClearChildrenInternal(bool immediate, IEnumerable<GameObject> children) {
            if (immediate) {
                children.ToList().ForEach(Object.DestroyImmediate);
            }
            else {
                children.ToList().ForEach(Object.Destroy);
            }
        }

        public static T Create<T>(this MonoBehaviour parent, Action<T> constructor) where T : Behaviour {
            var component = parent.gameObject.Create<T>(typeof(T).Name);
            constructor(component);
            return component;
        }

        public static T Create<T>(this MonoBehaviour parent, T template, string name = "") where T : Object {
            return parent.gameObject.Create(template, name);
        }

        public static GameObject Create(this MonoBehaviour parent, string name = "") {
            return parent.gameObject.Create(name);
        }

        public static T Create<T>(this MonoBehaviour parent, string name = "") where T : Behaviour {
            return parent.gameObject.Create<T>(name);
        }

        public static T Create<T>(this GameObject parent, string name = "") where T : Behaviour {
            return parent.Create(name).AddComponent<T>();
        }

        public static GameObject Create(this GameObject parent, string name = "") {
            GameObject gameObject;

            if (name.Length > 0) {
                gameObject = new GameObject(name);
            }
            else {
                gameObject = new GameObject();
            }

            gameObject.transform.SetParent(parent.transform, false);
            return gameObject;
        }

        public static T Create<T>(this GameObject parent, T template, string name = "") where T : Object {
            var gameObject = template as GameObject;
            var isBehavior = false;

            if (gameObject == null) {
                gameObject = template.As<MonoBehaviour>().gameObject;
                isBehavior = true;
            }

#if UNITY_EDITOR
            var prefab = PrefabUtility.GetPrefabParent(gameObject);

            if (prefab != null) {
                gameObject = prefab as GameObject;
            }

            gameObject = PrefabUtility.InstantiatePrefab(gameObject) as GameObject;
#else
            gameObject = Object.Instantiate(gameObject) as GameObject;
#endif

            gameObject.transform.SetParent(parent.transform, false);

            if (name.Length > 0) {
                gameObject.name = name;
            }

            if (isBehavior) {
                return gameObject.GetComponent<T>();
            }

            return gameObject as T;
        }

        public static bool IsPrefab(this GameObject target) {
#if UNITY_EDITOR
            if (PrefabUtility.GetPrefabType(target) == PrefabType.Prefab) {
                return true;
            }

            return PrefabUtility.GetPrefabParent(target) == null && PrefabUtility.GetPrefabObject(target) != null;
#else
            return false;
#endif
        }

        public static IEnumerable<T> FindAll<T>(this Component source) {
            return Object.FindObjectsOfType(typeof(T)).OfType<T>();
        }

        public static bool Within(this Transform from, Transform to, float distance) {
            return Within(from.position, to.position, distance);
        }

        public static bool Within(this Vector3 from, Vector3 to, float distance) {
            return Vector3.Distance(from, to) <= distance;
        }

        public static IEnumerable<Component> GetAllComponents(this Component source) {
            return source.GetComponents<Component>();
        }
    }
}