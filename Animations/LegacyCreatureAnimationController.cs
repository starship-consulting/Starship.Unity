using Starship.Unity.Core;
using Starship.Unity.Enumerations;
using Starship.Unity.Extensions;
using Starship.Unity.Models;
using UnityEngine;

namespace Starship.Unity.Animations {
    public class LegacyCreatureAnimationController : BaseComponent, IAnimationController {

        protected override void Awake() {
            base.Awake();
            Animation = GetComponent<Animation>();
        }

        //protected void Start() {
        //    StartIdling();
        //}

        protected void Update() {
            if (!IsDead && !Animation.isPlaying) {
                //Play(AnimationTypes.Idle);
            }
        }

        private float GetRemainingTime(AnimationClip clip) {
            return Animation[clip.name].length - Animation[clip.name].time;
        }
        
        /*private void Idle() {
            IdleLength = Play(IdleAnimations.GetRandomItem()).length;
            BeginFidget();
        }*/

        /*private void BeginFidget() {
            Run(Fidget, TimeSpan.FromSeconds(IdleLength*new Random().Next(1, 8)));
        }*/

        private void Fidget() {
            /*var clip = FidgetAnimations.GetRandomItem();
            var state = Play(clip, 1);

            Run(() => {
                AnimationController.Stop(clip.name);
                BeginFidget();
            }, TimeSpan.FromSeconds(state.length));*/
        }
        
        public void Play(AnimationDefinition animation) {
            /*if (type == AnimationTypes.Death) {
                IsDead = true;
            }

            StartAnimation(GetAnimationClip(type), GetLayer(type));*/
        }
        
        private int GetLayer(AnimationTypes type) {
            switch (type) {
                case AnimationTypes.Walk:
                case AnimationTypes.Face:
                    return 1;
                case AnimationTypes.Run:
                    return 1;
                case AnimationTypes.Block:
                    return 2;
                case AnimationTypes.Hit:
                    return 2;
                case AnimationTypes.Attack:
                    return 3;
                case AnimationTypes.Death:
                    return 99;
                default:
                    return 0;
            }
        }

        private AnimationClip GetAnimationClip(AnimationTypes type) {
            switch (type) {
                case AnimationTypes.Walk:
                case AnimationTypes.Face:
                    return WalkAnimations.GetRandom();
                case AnimationTypes.Run:
                    return RunAnimations.GetRandom();
                case AnimationTypes.Block:
                    return BlockAnimations.GetRandom();
                case AnimationTypes.Hit:
                    return HitAnimations.GetRandom();
                case AnimationTypes.Attack:
                    return AttackAnimations.GetRandom();
                case AnimationTypes.Death:
                    return DeathAnimations.GetRandom();
                default:
                    return IdleAnimations.GetRandom();
            }
        }
        
        private void StartAnimation(AnimationClip clip, int layer) {
            Animation[clip.name].wrapMode = layer <= 1 ? WrapMode.Loop : WrapMode.Once;

            if (!Animation.IsPlaying(clip.name) || layer >= 2) {
                Animation[clip.name].speed = 1;
                Animation[clip.name].time = 0;
                Animation[clip.name].layer = layer;
                Animation.CrossFade(clip.name, 0.5f, PlayMode.StopAll);
            }
        }

        public void Stop() {

        }

        private void StopAnimation(AnimationClip clip) {
            //Animation.Stop(clip.name);
            //Animation[clip.name].wrapMode = WrapMode.Once;
        }

        public AnimationClip[] WalkAnimations;

        public AnimationClip[] RunAnimations;

        public AnimationClip[] BlockAnimations;

        public AnimationClip[] HitAnimations;

        public AnimationClip[] IdleAnimations;

        public AnimationClip[] FidgetAnimations;

        public AnimationClip[] AttackAnimations;

        public AnimationClip[] DeathAnimations;
        
        private Animation Animation { get; set; }
        
        private float IdleLength = 0;

        private bool IsDead { get; set; }
    }
}