using Assets.Scripts.Core;
using Assets.Scripts.UI.Simple;

namespace Assets.Scripts.UI.HotkeyMenu {
    public class HotkeyMenuPresenter : BaseComponent {

        /*protected override void OnEnable() {
            base.OnEnable();
            UpdateState();
        }

        [Method(MethodDisplay.Button), Inspect]
        public void UpdateState() {

            this.ClearChildren();

            if (Template == null) {
                return;
            }

            foreach (var item in Items) {
                var instance = this.Create(Template);
                instance.name = item.Text;
                instance.Text.text = item.Text;
                instance.Image.sprite = item.Icon;

                if (item.Width > 0 || item.Height > 0) {
                    var width = item.Width > 0 ? item.Width : instance.Image.rectTransform.sizeDelta.x;
                    var height = item.Width > 0 ? item.Width : instance.Image.rectTransform.sizeDelta.x;
                    instance.Image.rectTransform.sizeDelta = new Vector2(width, height);
                }

                //instance.GetComponent<Button>().onClick.AddListener(() => {
                    
                //});
            }
        }

        private void SetupHotkey(Button button, HotkeyTypes hotkey) {
            button.onClick.AddListener(() => { Publish(new HotkeyTriggered(hotkey)); });
        }*/

        public HotkeyMenuItem[] Items;

        public SelectableLabelledIcon Template;
    }
}