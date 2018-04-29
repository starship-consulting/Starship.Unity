using Starship.Unity.Entities;
using Starship.Unity.ScriptableObjects;
using UnityEngine;

namespace Starship.Unity.Types {

    [CreateAssetMenu(menuName = "ScriptableObjects/GameType")]
    public class GameType : BaseScriptableObject {

        public GameObject[] Components;

        public RuntimeProperty[] Properties;
    }
}