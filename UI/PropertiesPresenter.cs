﻿using System.Collections.Generic;
using Assets.Scripts.Components;
using Assets.Scripts.Core;
using Assets.Scripts.Definitions;
using Assets.Scripts.Enumerations;
using Assets.Scripts.Extensions;

namespace Assets.Scripts.UI {
    public class PropertiesPresenter : BaseComponent {

        protected override void Awake() {
            base.Awake();
            this.ClearChildren();
            Properties = new Dictionary<PropertyTypes, PropertyPresenter>();
        }
        
        public void SetProperties(params CharacterProperty[] properties) {
            foreach (var property in properties) {
                OnPropertyChanged(property);
            }
        }
        
        private void OnPropertyChanged(CharacterProperty property) {
            if (!Properties.ContainsKey(property.Type)) {
                Properties.Add(property.Type, this.Create(Template));
            }

            Properties[property.Type].SetProperty(property, property.GetIcon(PropertyManager));
        }
        
        public PropertyPresenter Template;

        public PropertyManager PropertyManager;

        private Dictionary<PropertyTypes, PropertyPresenter> Properties { get; set; }
    }
}