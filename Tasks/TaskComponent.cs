using Assets.Scripts.Core;
using Assets.Scripts.Enumerations;
using Assets.Scripts.Events;
using Assets.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Tasks {
    public abstract class TaskComponent : BaseComponent, IsTask {

        public T GetModel<T>() {
            return Model.GetComponent<T>();
        }

        public void Begin(MonoBehaviour model) {
            Model = model;

            if (Status == StatusTypes.Paused) {
                Status = StatusTypes.Running;

                try {
                    OnBegin(model);
                }
                catch {
                    Status = StatusTypes.Finished;
                    throw;
                }
            }
        }

        public void Finish() {
            if (Status == StatusTypes.Running) {
                Status = StatusTypes.Finished;

                if (Next != null) {
                    Next.Begin(Model);
                }
                else {
                    ExecuteEvents.Execute<IsTaskObserver>(gameObject, null, (target, data) => {
                        target.OnAllTasksFinished();
                    });
                }
            }
        }

        protected abstract void OnBegin(MonoBehaviour model);

        public UnityEvent Finished;

        public StatusTypes Status { get; set; }

        public TaskComponent Next;

        public MonoBehaviour Model { get; private set; }
    }
}