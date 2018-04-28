using System;
using UnityEngine;

namespace Assets.Scripts.Players {

    [Serializable]
    public class PartyMember {
        public string Name;

        public Sprite Portrait;

        public int Body;

        public int Mobility;

        public int Mind;
    }
}