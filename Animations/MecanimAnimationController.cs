using System;
using System.Collections;
using System.Linq;
using Assets.Scripts.Core;
using Assets.Scripts.Extensions;
using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Animations {
    public class MecanimAnimationController : BaseComponent, IAnimationController {

        /*public void Play(AnimationDefinition definition) {

            Stop();

            if (definition.Name == DefaultAnimation.Name) {
                return;
            }

            Animator.avatar = definition.Avatar;
            Animator.runtimeAnimatorController = definition.Controller;

            CurrentAnimation = definition.Name;
            IsAnimating = true;
            SetState(CurrentAnimation, true);
        }

        public void Stop() {
            if (Animator == null) {
                return;
            }

            if (IsAnimating) {
                Animator.SetBool(CurrentAnimation, false);
                IsAnimating = false;
                CurrentAnimation = string.Empty;
            }
        }*/
        
        public void Play(AnimationDefinition definition) {
        }

        public void Stop() {
        }

        public bool IsTriggered(string key) {
            return GetAnimator().GetBool(key);
        }

        public void Trigger(string key) {
            GetAnimator().SetTrigger(key);
        }

        public void ActivateState(string key) {
            SetState(key, true);
        }

        public void SetState(string key, float value) {
            if (value == 0 && AnimationStates.Contains(key)) {
                AnimationStates = AnimationStates.Where(each => each != key).ToArray();
            }
            else if (value != 0 && !AnimationStates.Contains(key)) {
                AnimationStates = AnimationStates.Add(key);
            }

            GetAnimator().SetFloat(key, value);
        }

        public void SetState(string key, bool value) {
            if (!value && AnimationStates.Contains(key)) {
                AnimationStates = AnimationStates.Where(each => each != key).ToArray();
            }
            else if (value && !AnimationStates.Contains(key)) {
                AnimationStates = AnimationStates.Add(key);
            }

            GetAnimator().SetBool(key, value);
        }

        public void SetState(string key, float value, Action finishedCallback) {
            SetState(key, value);
            StartCoroutine(WaitForAnimation(finishedCallback));
        }

        public void SetState(string key, bool value, Action finishedCallback) {
            SetState(key, value);
            StartCoroutine(WaitForAnimation(finishedCallback));
        }

        private IEnumerator WaitForAnimation(Action finishedCallback) {
            yield return new WaitForAnimation(GetAnimator());
            finishedCallback();
        }

        private Animator GetAnimator() {
            if (Animator == null) {
                Animator = this.FindComponent<Animator>();
            }

            return Animator;
        }

        public string[] AnimationStates;

        //public AnimationDefinition Animation;

        public Animator Animator;

        public string CurrentAnimation;
    }
}