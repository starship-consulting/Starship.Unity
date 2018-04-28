using System;
using UnityEngine;

namespace Assets.Scripts.Skills {
    
    [Serializable]
    public class SkillManager : ScriptableObject {

        public SkillDefinition[] Definitions;
    }
}