using UnityEngine.UI;

namespace Assets.Scripts.UI.Tables {
    public class TableInputCellView : TableCellView {

        private void Awake() {
            Input = GetComponentInChildren<InputField>();
            Input.onValueChange.AddListener(OnValueChanged);
        }

        private void OnValueChanged(string value) {
            //Change(value);
        }

        public override void SetContent(object content) {
            var field = GetComponent<InputField>();
            field.text = content != null ? content.ToString() : string.Empty;
        }

        private InputField Input { get; set; }
    }
}