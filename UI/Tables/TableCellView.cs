using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Tables {
    public class TableCellView : MonoBehaviour {

        private void Awake() {
            Text = GetComponentInChildren<Text>();
        }

        public virtual void SetContent(object content) {
            Text.text = content != null ? content.ToString() : string.Empty;
        }

        protected void Change(object value) {
            if (Changed != null) {
                Changed(this, value);
            }
        }

        public event Action<TableCellView, object> Changed;

        private Text Text { get; set; }
    }
}