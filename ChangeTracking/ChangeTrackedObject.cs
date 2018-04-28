using System;
using System.ComponentModel;
using System.Linq;

namespace Assets.Scripts.ChangeTracking {
    public abstract class ChangeTrackedObject : INotifyPropertyChanged {
        protected ChangeTrackedObject() {
            Tracker = new ChangeTracker(this);
        }

        public void Edit(Action editAction) {
            editAction();

            if (PropertyChanged != null) {
                var changeset = Tracker.GetChanges().ToList();

                if (changeset.Any()) {
                    foreach (var change in changeset) {
                        PropertyChanged(this, new PropertyChangedEventArgs(change.Field.Name));
                    }

                    if (Edited != null) {
                        Edited();
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public event Action Edited;

        private ChangeTracker Tracker { get; set; }
    }
}