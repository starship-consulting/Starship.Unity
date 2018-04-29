using Starship.Unity.Core;
using UnityEngine;

namespace Starship.Unity.Databinding {
    public class BindingContext : MonoBehaviour {

        public void Bind(object model) {
            Model = model;
        }

        public SerializableType Type;

        public object Model;
    }
}