using System.Collections.Generic;
using Starship.Unity.Components;
using Starship.Unity.Core;
using Starship.Unity.Definitions;
using Starship.Unity.Enumerations;
using Starship.Unity.Extensions;

namespace Starship.Unity.UI {
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