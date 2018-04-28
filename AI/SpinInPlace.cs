using Assets.Scripts.Core;
using Assets.Scripts.Movement;

namespace Assets.Scripts.AI {
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