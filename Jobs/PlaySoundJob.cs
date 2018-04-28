using System;
using UnityEngine;

namespace Assets.Scripts.Jobs {

    [Serializable]
    public class PlaySoundJob : Job {

        public float Volume;

        public AudioClip Clip;
    }
}