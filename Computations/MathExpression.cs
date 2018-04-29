using System;
using Starship.Unity.Actors;

namespace Starship.Unity.Computations {

    [Serializable]
    public class MathExpression {

        public float ComputeValue(Actor actor) {
            float value = 0;
            /*var property = actor.GetProperty(Property);

            if(property != null) {
                value = property.Value;
            }

            if(Operator != OperatorTypes.Constant && Operand != null) {

                var computedValue = Operand.ComputeValue(actor);

                switch(Operator) {
                    case OperatorTypes.Add:
                        value += computedValue;
                        break;
                    case OperatorTypes.Subtract:
                        value -= computedValue;
                        break;
                    case OperatorTypes.Divide:
                        if(value > 0 && computedValue > 0) {
                            value /= computedValue;
                        }

                        break;
                    case OperatorTypes.Multiply:
                        value *= computedValue;
                        break;
                }
            }*/

            return value;
        }

        private bool CanEditOperand() {
            return Operator != OperatorTypes.Constant;
        }

        //public PropertyType Property;

        public OperatorTypes Operator;
        
        public Algorithm Operand;
    }
}