using System.Reflection;

namespace Starship.Unity.ChangeTracking {
    public class ChangeTrackerField {

        public ChangeTrackerField(FieldInfo field) {
            Field = field;
        }

        public bool HasChanged(object source) {
            OldValue = NewValue;
            NewValue = Field.GetValue(source);

            if (OldValue != null) {
                return !OldValue.Equals(NewValue);
            }

            if (NewValue != null) {
                return !NewValue.Equals(OldValue);
            }
            
            return false;
        }

        public FieldInfo Field { get; set; }

        public object OldValue { get; set; }

        public object NewValue { get; set; }
    }
}