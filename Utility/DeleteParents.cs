using Assets.Scripts.Core;
using Assets.Scripts.Extensions;

namespace Assets.Scripts.Utility {
    public class DeleteParents : BaseComponent {
        protected override void Start() {
            base.Start();
            gameObject.DeleteParents();
        }
    }
}