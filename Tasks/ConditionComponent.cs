using System;
using Assets.Scripts.Core;

namespace Assets.Scripts.Tasks {
    public abstract class ConditionComponent : BaseComponent {

        //public void Hook() {
        //}

        public event Action<bool> ResultChanged;
    }
}