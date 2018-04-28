using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI {
    public class Datasource : MonoBehaviour {

        public IEnumerable<T> GetData<T>() {
            return GetComponentsInChildren<T>();
        }
    }
}