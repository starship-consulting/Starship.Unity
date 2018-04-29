using System;
using UnityEngine;

namespace Starship.Unity.Skills {
    
    [Serializable]
    public class SkillManager : ScriptableObject {

        public SkillDefinition[] Definitions;
    }
}