using Assets.Scripts.Elements;
using UnityEngine;

namespace Assets.Scripts.Crafting {
    public class CraftComponent : ScriptableObject {

        public string Name;

        public ElementType RequiredMaterialType;
    }
}