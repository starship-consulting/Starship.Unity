using System.Collections.Generic;
using Starship.Unity.Core;

namespace Starship.Unity.Databinding {
    public abstract class DataSource<T> : BaseComponent {

        public abstract List<T> GetData();
    }
}