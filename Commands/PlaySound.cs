﻿using Starship.Unity.Audio;
using UnityEngine;

namespace Starship.Unity.Commands {
    public class PlaySound {
        public PlaySound() {
        }

        public PlaySound(Sound sound) {
            Sound = sound;
        }

        public PlaySound(Sound sound, Vector3 position) {
            Sound = sound;
            Position = position;
        }

        public Sound Sound { get; set; }

        public Vector3 Position { get; set; }

        public Collision Collision { get; set; }
    }
}