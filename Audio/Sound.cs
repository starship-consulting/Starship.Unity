using UnityEngine;

namespace Assets.Scripts.Audio {
    public class Sound : MonoBehaviour {
        
        public Collision Collision;

        public AudioClip[] Clips;

        public float Volume = 1f;

        public float MinPitch = 1f;

        public float MaxPitch = 1f;

        public float SpatialBlend = 1f;

        public bool Loop = false;
    }
}