using System;
using Assets.Scripts.Actors;
using UnityEngine;

namespace Assets.Scripts.Computations {

    [CreateAssetMenu(menuName = "ScriptableObjects/Algorithm")]
    public class Algorithm : ScriptableObject {

        public float ComputeValue(Actor actor) {
            return Expression.ComputeValue(actor);
        }
        
        public MathExpression Expression;
    }
}