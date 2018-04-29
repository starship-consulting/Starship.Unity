using Starship.Unity.Actors;
using UnityEngine;

namespace Starship.Unity.Computations {

    [CreateAssetMenu(menuName = "ScriptableObjects/Algorithm")]
    public class Algorithm : ScriptableObject {

        public float ComputeValue(Actor actor) {
            return Expression.ComputeValue(actor);
        }
        
        public MathExpression Expression;
    }
}