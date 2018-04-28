﻿using Assets.Scripts.Core;
using Assets.Scripts.Extensions;

namespace Assets.Scripts.UI {
    public abstract class DataContext<T> : BaseComponent {

        protected override void OnEnable() {
            base.OnEnable();
            UpdateState();
        }
        
        public void UpdateState() {
            //DataSource.Invoke(this);
        }

        public void Set(params T[] items) {
            this.ClearChildren();
            Items = items;
            UpdateBoundState();
        }

        protected abstract void UpdateBoundState();

        //public ActionBinding DataSource;

        protected T[] Items { get; set; }
    }
}