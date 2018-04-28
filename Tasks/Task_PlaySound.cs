using Assets.Scripts.Audio;
using Assets.Scripts.Commands;
using UnityEngine;

namespace Assets.Scripts.Tasks {
    public class Task_PlaySound : TaskComponent {

        protected override void OnBegin(MonoBehaviour model) {
            Publish(new PlaySound(Sound, model.transform.position));
            Finish();
        }

        public Sound Sound;
    }
}