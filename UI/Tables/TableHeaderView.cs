using Assets.Scripts.Extensions;

namespace Assets.Scripts.UI.Tables {
    public class TableHeaderView : TableRowView {

        public void Clear() {
            this.ClearChildren();
        }
    }
}