using Assets.Scripts.Entities;
using Assets.Scripts.ScriptableObjects;
using UnityEngine;

namespace Assets.Scripts.Types {

    [CreateAssetMenu(menuName = "ScriptableObjects/GameType")]
    public class GameType : BaseScriptableObject {

        public GameObject[] Components;

        public RuntimeProperty[] Properties;
    }
}