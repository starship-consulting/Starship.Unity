using System;
using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.Databinding {
    public class BindingContext : MonoBehaviour {

        public void Bind(object model) {
            Model = model;
        }

        public SerializableType Type;

        public object Model;
    }
}