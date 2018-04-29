using Starship.Unity.Core;
using Starship.Unity.Extensions;
using Starship.Unity.Movement;
using Starship.Unity.Scheduling;

namespace Starship.Unity.AI {

    public class Wandering : BaseComponent {

        protected override void OnEnable() {
            base.OnEnable();

            Controller = GetComponent<IsMovementController>();
        }

        protected override void Start() {
            base.Start();
            NextMove();
        }
        
        public void NextMove() {
            if (!isActiveAndEnabled) {
                return;
            }
            
            Commands.Add(() => {
                var moves = Controller.GetValidMoves();

                if (moves.Count <= 0) {
                    return Promise.Empty;
                }

                var move = moves.GetRandom();

                return Controller.Face(move).Then(() => Controller.Move(move)).Then(() => NextMove());
            });
        }

        private IsMovementController Controller;

        private readonly CommandQueue Commands = new CommandQueue();
    }
}