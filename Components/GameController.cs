using System;
using System.Collections.Generic;
using Assets.Scripts.Core;
using Assets.Scripts.Events;
using UnityEngine;

namespace Assets.Scripts.Components {

    [ExecuteInEditMode]
    public class GameController : BaseComponent {

        protected override void OnEnable() {
            base.OnEnable();

            On<UIRefreshRequested>(OnUIRefreshRequested);
        }

        private void OnUIRefreshRequested(UIRefreshRequested e) {
            RefreshQueue.Add(e);
        }

        public void Update() {
            foreach (var item in RefreshQueue) {
                item.Source.Repaint();
            }

            RefreshQueue.Clear();
        }
        
        private List<UIRefreshRequested> RefreshQueue = new List<UIRefreshRequested>();
    }
}