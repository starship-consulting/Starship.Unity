using System;
using System.Collections.Generic;
using Assets.Scripts.Core;

namespace Assets.Scripts.Databinding {
    public abstract class DataSource<T> : BaseComponent {

        public abstract List<T> GetData();
    }
}