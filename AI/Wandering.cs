using Assets.Scripts.Core;
using Assets.Scripts.Extensions;
using Assets.Scripts.Movement;
using Assets.Scripts.Scheduling;

namespace Assets.Scripts.AI {

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