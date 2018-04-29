using Starship.Unity.Core;
using Starship.Unity.Movement;

namespace Starship.Unity.AI {
    public class SpinInPlace : BaseComponent {

        protected override void OnEnable() {
            base.OnEnable();

            Controller = GetComponent<GridMovement>();
        }

        protected override void Start() {
            base.Start();
            NextMove();
        }
        
        public void NextMove() {
            Controller.FaceEast().Then(()=> NextMove());
        }

        private GridMovement Controller;
    }
}