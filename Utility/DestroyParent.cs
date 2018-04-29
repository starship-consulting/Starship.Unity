using Starship.Unity.Core;

namespace Starship.Unity.Utility {
    public class DestroyParent : BaseComponent {

        protected override void Start() {
            base.Start();

            if(gameObject.transform.parent == null) {
                return;
            }

            if (AfterSeconds > 0) {
                Destroy(gameObject.transform.parent.gameObject, AfterSeconds);
            }
            else {
                Destroy(gameObject.transform.parent.gameObject);
            }
        }

        public float AfterSeconds;
    }
}