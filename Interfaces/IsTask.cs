using UnityEngine;

namespace Assets.Scripts.Interfaces {
    public interface IsTask {
        void Begin(MonoBehaviour model);
        void Finish();
    }
}