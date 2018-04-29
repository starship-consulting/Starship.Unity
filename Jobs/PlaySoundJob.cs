using System;
using UnityEngine;

namespace Starship.Unity.Jobs {

    [Serializable]
    public class PlaySoundJob : Job {

        public float Volume;

        public AudioClip Clip;
    }
}