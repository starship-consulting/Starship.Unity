using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Starship.Unity.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Starship.Unity.UI.Tables {
    public class TableView : MonoBehaviour {

        private void Awake() {
            _BindingProperties = new List<PropertyInfo>();
            Rows = new Dictionary<object, TableRowView>();
        }

        private void Start() {
            GetComponent<VerticalLayoutGroup>().padding = new RectOffset(CellBorder, CellBorder, CellBorder, CellBorder);
            Header.SetCellBorder(CellBorder);
        }

        public void Clear() {
            Header.Clear();
            _BindingProperties.Clear();
            Rows.Keys.ToList().ForEach(DeleteItem);
        }

        public void AddItem(object item) {
            InternalUpdateItem(item);
        }

        public void UpdateItem(object item) {
            InternalUpdateItem(item);
        }

        public void DeleteItem(object item) {
            if (Rows.ContainsKey(item)) {
                Destroy(Rows[item].gameObject);
                Rows.Remove(item);
            }
        }

        private void InternalUpdateItem(object item) {
            if (!Rows.ContainsKey(item)) {
                var row = this.Create(RowTemplate, "Row" + (Rows.Count + 1));
                row.SetCellBorder(CellBorder);

                Rows.Add(item, row);
            }

            var cells = GetBindingProperties(item).Select(each => each.GetValue(item, null)).ToList();
            Rows[item].Bind(cells);
        }

        public void Bind(IEnumerable data) {

            Clear();

            if (data == null) {
                return;
            }

            data.Cast<object>().ToList().ForEach(AddItem);
        }

        public void SetHeaders(IEnumerable<string> headers) {
            foreach (var column in headers) {
                Header.AddCell(column);
            }
        }

        private List<PropertyInfo> GetBindingProperties(params object[] data) {
            if (!_BindingProperties.Any()) {
                if (data.Any()) {
                    _BindingProperties = data.First().GetType().GetProperties().Reverse().ToList();
                    SetHeaders(_BindingProperties.Select(each => each.Name));
                }
            }

            return _BindingProperties;
        } 

        public int CellBorder;

        public TableHeaderView Header;

        public TableRowView RowTemplate;

        private List<PropertyInfo> _BindingProperties { get; set; }

        private Dictionary<object, TableRowView> Rows { get; set; } 
    }
}