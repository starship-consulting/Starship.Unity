using System;
using System.Collections;
using System.Collections.Generic;
using Starship.Unity.Enumerations;
using Starship.Unity.Events;
using Starship.Unity.Events.Models;
using Starship.Unity.Extensions;
using Starship.Unity.Scheduling;
using Starship.Unity.Utilities;
using UnityEngine;
using UnityEngine.EventSystems;
using Component = UnityEngine.Component;

namespace Starship.Unity.Core {
    
    public abstract class BaseComponent : MonoBehaviour {

        protected virtual void OnValidate() {
            
        }

        protected virtual void Awake() {
            Automapper.AutomapRequiredComponents(this);
        }

        protected virtual void OnEnable() {
            IsDisabling = false;
            typeof(ComponentEvents<>).MakeGenericType(GetType()).GetMethod("Enable").Invoke(null, new object[] {this});
            Send(this);
        }

        protected virtual void Start() {
            SetComponentState(ComponentStates.Started);
            typeof(ComponentEvents<>).MakeGenericType(GetType()).GetMethod("Start").Invoke(null, new object[] {this});
        }

        private void SetComponentState(ComponentStates state) {
            ExecuteEvents.Execute<IsComponentObserver>(gameObject, null, (target, data) => target.OnComponentStateChanged(new ComponentStateChanged(this, state)));
            //ComponentState = state;
        }

        public Promise AsPromise() {
            return new Promise(this);
        }

        public void DeleteParents() {
            gameObject.DeleteParents();
        }

        public void DeleteObject() {
            gameObject.Delete();
        }

        public void Abort(bool destroy = true) {
            SetComponentState(ComponentStates.Aborted);

            if (OnAborted != null) {
                OnAborted(this);
            }

            if (destroy) {
                this.Destroy();
            }
        }

        public void Delete() {
            DetachEvents();
            this.Destroy();
        }

        public T Add<T>() where T : Component {
            return this.AddComponent<T>();
        }

        public T Add<T>(Action<T> constructor) where T : Component {
            var component = this.AddComponent<T>();
            constructor(component);
            return component;
        }

        public Component Add(Type type) {
            return gameObject.AddComponent(type);
        }

        protected virtual void OnDisable() {
            if (IsDisabling) {
                return;
            }

            IsDisabling = true;
            typeof(ComponentEvents<>).MakeGenericType(GetType()).GetMethod("Disable").Invoke(null, new object[] {this});
            DetachEvents();
            OnStopped();
        }

        protected virtual void OnDestroy() {
            DetachEvents();
            SetComponentState(ComponentStates.Destroyed);
            OnStopped();

            if (OnDestroyed != null) {
                OnDestroyed(this);
            }
        }

        protected virtual void OnStopped() {
        }

        protected void DetachEvents() {
            if (IsDestroying || IsDisabling) {
                return;
            }

            IsDestroying = true;

            if (EventIdentifiers != null) {
                foreach (var id in EventIdentifiers) {
                    EventHub.Off(id);
                }

                EventIdentifiers.Clear();
            }

            // Detach observed components
            if (ObservedComponents != null) {
                foreach (var component in ObservedComponents) {
                    component.Off(this);
                }

                ObservedComponents.Clear();
            }

            // Detach subscribers
            if (Subscribers != null) {
                foreach (var subscriber in Subscribers) {
                    subscriber.Key.Disconnect(this);
                    subscriber.Value.Clear();
                }

                Subscribers.Clear();
            }

            //foreach (var e in GetType().GetEvents()) {
            //    e.ClearEvents(this);
            //}
        }

        public void Publish<I>(Action<I> action) where I : IEventSystemHandler {
            ExecuteEvents.Execute<I>(gameObject, null, (arg1, data) => { action(arg1); });
        }

        public void Publish<E>(E e) {
            Send(e);
            EventHub.Publish(e);
        }

        public void Send(object e) {
            if (Subscribers != null) {
                foreach (var subscriber in Subscribers) {
                    subscriber.Value.Publish(e);
                }
            }
        }

        public void On<E>(BaseComponent target, Action<E> callback) {
            if (target.Subscribers == null) {
                target.Subscribers = new Dictionary<BaseComponent, TypeRouter>();
            }

            if (ObservedComponents == null) {
                ObservedComponents = new List<BaseComponent>();
            }

            if (!target.Subscribers.ContainsKey(this)) {
                target.Subscribers.Add(this, new TypeRouter());
            }

            ObservedComponents.Add(target);
            target.Subscribers[this].On(callback);
        }

        public void On<E>(Action<E> callback) {
            if (EventIdentifiers == null) {
                EventIdentifiers = new List<Guid>();
            }

            var id = EventHub.On(callback);
            EventIdentifiers.Add(id);
        }

        public void Disconnect(BaseComponent observedComponent) {
            if (ObservedComponents != null) {
                ObservedComponents.Remove(observedComponent);
            }
        }

        public void Off(BaseComponent subscriber) {
            if (Subscribers != null) {
                if (Subscribers.ContainsKey(subscriber)) {
                    Subscribers[subscriber].Clear();
                }
            }
        }

        public void NextFrame(Action action) {
            StartCoroutine(OnNextFrame(action));
        }

        private IEnumerator OnNextFrame(Action action) {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

            action();
        }

        public ScheduledTask Run(Action action, TimeSpan delay, bool repeat = false) {
            return GetTaskScheduler().Run(this, action, delay, repeat);
        }

        public Promise Run(Action action) {
            var promise = new Promise(action);
            promise.Run();
            return promise;
        }

        public TaskScheduler GetTaskScheduler() {
            return this.GetOrAdd<TaskScheduler>();
        }

        //public ComponentStates ComponentState { get; set; }

        public event Action<BaseComponent> OnDestroyed;

        public event Action<BaseComponent> OnAborted;

        protected bool IsDisabling { get; set; }

        protected bool IsDestroying { get; set; }

        private List<Guid> EventIdentifiers { get; set; }

        private Dictionary<BaseComponent, TypeRouter> Subscribers { get; set; }

        private List<BaseComponent> ObservedComponents { get; set; }
    }
}