using Assets.Scripts.Core;
using Starship.Unity.Plugins.SSAOPro;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

namespace Assets.Scripts.Systems {
    public class GraphicSystem : BaseComponent {

        protected override void Start() {
            base.Start();
            
            UpdateComponent<Antialiasing>(EnableAntiAliasing);
            UpdateComponent<SSAOPro>(EnableSSAO);
            UpdateComponent<DepthOfField>(EnableDepthOfField);
            UpdateComponent<GlobalFog>(EnableGlobalFog);
        }

        private void UpdateComponent<T>(bool isEnabled) where T : MonoBehaviour {
            var component = Camera.main.GetComponent<T>();
            component.enabled = isEnabled;
        }
        
        public bool EnableAntiAliasing = true;

        public bool EnableSSAO = true;

        public bool EnableDepthOfField = true;

        public bool EnableGlobalFog = true;
    }
}