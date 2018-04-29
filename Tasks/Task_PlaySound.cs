using Starship.Unity.Audio;
using Starship.Unity.Commands;
using UnityEngine;

namespace Starship.Unity.Tasks {
    public class Task_PlaySound : TaskComponent {

        protected override void OnBegin(MonoBehaviour model) {
            Publish(new PlaySound(Sound, model.transform.position));
            Finish();
        }

        public Sound Sound;
    }
}