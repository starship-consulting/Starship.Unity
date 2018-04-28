using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Assets.Scripts.Core;

namespace Assets.Scripts.ChangeTracking {
    public class ChangeTracker {

        static ChangeTracker() {
            FieldCache = new Dictionary<Type, List<FieldInfo>>();
        }

        public ChangeTracker(object target) {
            Target = target;

            var type = Target.GetType();

            lock (FieldCache) {
                if (!FieldCache.ContainsKey(type)) {
                    FieldCache.Add(type, type.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy).ToList());
                }
            }

            Fields = FieldCache[type].Select(each => new ChangeTrackerField(each)).ToList();
            GetChanges();
        }
        
        public IEnumerable<ChangeTrackerField> GetChanges() {
            return Fields.Where(each => each.HasChanged(Target));
        }
        
        public object Target { get; set; }
        
        private List<ChangeTrackerField> Fields { get; set; }
        
        private static Dictionary<Type, List<FieldInfo>> FieldCache { get; set; }
    }
}