﻿using System.Collections.Generic;
using System.Linq;

namespace Starship.Unity.ChangeTracking {
    public class ChangeTrackerState {

        public ChangeTrackerState(List<ChangeTrackerProperty> changes) {
            Changes = changes;
        }

        public List<ChangeTrackerProperty> Changes { get; set; }

        public bool HasChanges { get { return Changes.Any(); } }
    }
}