using System.Collections.Generic;
using System.Linq;
using Starship.Unity.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Starship.Unity.UI.Tables {
    public class TableRowView : MonoBehaviour {

        private void Awake() {
            Cells = new List<TableCellView>();
        }

        public void SetCellBorder(int value) {
            GetComponent<HorizontalLayoutGroup>().padding = new RectOffset(value, value, value, 0);
            GetComponent<HorizontalLayoutGroup>().spacing = value;
        }

        public void AddCell(object header) {
            var cell = this.Create(CellTemplate);
            Cells.Add(cell);
            cell.SetContent(header);
        }

        private void CellValueChanged() {
            
        }

        public void Bind(IEnumerable<object> items) {
            if (!Cells.Any()) {
                foreach (var item in items) {
                    AddCell(item);
                }
            }
            else {
                var index = 0;

                foreach (var item in items) {
                    Cells[index].SetContent(item);
                    index += 1;
                }
            }
        }

        public TableCellView CellTemplate;

        private List<TableCellView> Cells { get; set; } 
    }
}