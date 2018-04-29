using System.Collections.Generic;
using UnityEngine;

namespace Starship.Unity.UI {
    public class Datasource : MonoBehaviour {

        public IEnumerable<T> GetData<T>() {
            return GetComponentsInChildren<T>();
        }
    }
}