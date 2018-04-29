namespace Starship.Unity.UI {
    /*public class DataView : MonoBehaviour {

        private void Awake() {
            AddButton.onClick.AddListener(OnAddData);

            var field = TypeDropdown.GetComponent<UISelectField>();
            field.onChange.AddListener(OnChanged);

            foreach (var type in TypeCache.GetTypesOf<Entity>(false)) {
                field.AddOption(type.Name);
            }
        }

        private void OnAddData() {
            if (SelectedType == null) {
                return;
            }

            Database.New(SelectedType);
        }

        private void OnChanged(int index, string value) {
            SelectedType = TypeCache.Lookup(value);
            var data = Database.Get(SelectedType);

            Table.Bind(data);

            if (NewEventToken != null) {
                NewEventToken.Unregister();
                UpdateEventToken.Unregister();
                NewEventToken = null;
                UpdateEventToken = null;
            }

            NewEventToken = Database.OnNew(SelectedType, Table.AddItem);
            UpdateEventToken = Database.OnUpdate(SelectedType, Table.AddItem);
        }

        public Button AddButton;

        public UISelectField TypeDropdown;

        public TableView Table;

        private EventToken NewEventToken { get; set; }

        private EventToken UpdateEventToken { get; set; }

        private Type SelectedType { get; set; }
    }*/
}