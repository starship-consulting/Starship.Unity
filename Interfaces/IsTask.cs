using UnityEngine;

namespace Starship.Unity.Interfaces {
    public interface IsTask {
        void Begin(MonoBehaviour model);
        void Finish();
    }
}