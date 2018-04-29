using System;
using Starship.Unity.Core;

namespace Starship.Unity.Tasks {
    public abstract class ConditionComponent : BaseComponent {

        //public void Hook() {
        //}

        public event Action<bool> ResultChanged;
    }
}