using Starship.Unity.Core;
using Starship.Unity.Extensions;

namespace Starship.Unity.Utility {
    public class DeleteParents : BaseComponent {
        protected override void Start() {
            base.Start();
            gameObject.DeleteParents();
        }
    }
}