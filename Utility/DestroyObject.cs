using Starship.Unity.Core;

namespace Starship.Unity.Utility {
    public class DestroyObject : BaseComponent {

        protected override void Start() {
            base.Start();

            if (AfterSeconds > 0) {
                Destroy(gameObject, AfterSeconds);
            }
            else {
                Destroy(gameObject);
            }
        }

        public float AfterSeconds;
    }
}