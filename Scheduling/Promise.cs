using System;
using Assets.Scripts.Core;
using Assets.Scripts.Enumerations;

namespace Assets.Scripts.Scheduling {
    public class Promise : IsContinuable {
        public Promise() {
        }

        public Promise(BaseComponent component) {
            component.OnDestroyed += OnDestroyed;
            component.OnAborted += OnAborted;
            Run();
        }

        public Promise(Action action) {
            Action = () => {
                action();
                Finish();
            };
        }

        public Promise(Func<Promise> factory) {
            Action = () => {
                Then(factory());
                Finish();
            };
        }

        private void OnAborted(BaseComponent component) {
            component.OnAborted -= OnAborted;
            Abort();
        }

        private void OnDestroyed(BaseComponent component) {
            component.OnDestroyed -= OnDestroyed;
            Finish();
        }

        public void SetProgress(float progress) {
            if (progress > 1) {
                progress = 1;
            }
            else if (progress < 0) {
                progress = 0;
            }

            if (Progress != progress) {
                Progress = progress;

                if (ProgressChanged != null) {
                    ProgressChanged(progress);
                }
            }
        }

        public Promise When(Func<float, bool> condition, Func<Promise> promise) {
            Action<float> callback = null;

            callback = (progress) => {
                if (condition(progress)) {
                    ProgressChanged -= callback;
                    promise();
                }
            };

            ProgressChanged += callback;

            return this;
        }

        public Promise Then(Action action) {
            return Then(new Promise(action));
        }

        public Promise Then(Func<Promise> factory) {
            return Then(new Promise(factory));
        }

        public Promise Then(Promise promise) {
            GetContinuation().Continuation = promise;
            return this;
        }

        private Promise GetContinuation() {
            return Continuation != null ? Continuation.GetContinuation() : this;
        }

        public void Run() {
            if (Status == StatusTypes.Paused) {
                Status = StatusTypes.Running;

                if (Action != null) {
                    Action();
                    Action = null;
                }
            }
        }

        public void Abort() {
            if (Status == StatusTypes.Running) {
                OnFinished();
            }
        }

        public void Finish() {
            if (Status == StatusTypes.Running) {
                if (Continuation != null && Continuation.Status != StatusTypes.Finished) {
                    Continuation.Finished += OnFinished;
                    Continuation.Run();
                    return;
                }

                OnFinished();
            }
        }

        private void OnFinished(Promise promise) {
            Continuation = null;
            promise.Finished -= OnFinished;
            OnFinished();
        }

        private void OnFinished() {
            Status = StatusTypes.Finished;

            if (Finished != null) {
                Finished(this);
            }
        }

        public static Promise Empty = new Promise {Status = StatusTypes.Finished};

        public Action Action { get; set; }

        public float Progress { get; private set; }

        public StatusTypes Status { get; set; }

        public event Action<Promise> Finished;

        public event Action<float> ProgressChanged;

        public Promise Continuation { get; set; }
    }
}