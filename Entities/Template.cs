using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Types;
//using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Entities {

    [CreateAssetMenu(menuName = "ScriptableObjects/Template")]
    public class Template : BaseScriptableObject {

        public string Name;
        
        //[OnInspectorGUI("OnInspector"), OnValueChanged("OnFlagsChanged")]
        public GameType[] Flags;

        //[CustomValueDrawer("DrawProperties")]
        //[ListDrawerSettings(DraggableItems = false, ShowItemCount = false, HideAddButton = true, Expanded = true, IsReadOnly = true)]
        public PropertyValue[] Properties;

        public Dictionary<string, PropertyValue> TestFields;

        private void OnInspector() {
            RemoveInvalidProperties();
            AddMissingProperties();
        }

        private void OnFlagsChanged() {
            RemoveInvalidProperties();
            AddMissingProperties();
        }

        private void RemoveInvalidProperties() {
            var properties = Flags.SelectMany(each => each.Properties).Where(each => each != null).ToList();
            Properties = Properties.Where(each => properties.Any(property => property.Name == each.Name)).ToArray();
        }

        private void AddMissingProperties() {
            Properties = Flags.SelectMany(each => each.Properties)
                .Where(each => Properties.All(property => property.Name != each.Name))
                .Select(each => new PropertyValue { Name = each.Name })
                .Concat(Properties)
                .ToArray();
        }

#if UNITY_EDITOR
        private PropertyValue DrawProperties(PropertyValue property, GUIContent label) {
            var flag = Flags.SelectMany(each => each.Properties).FirstOrDefault(each => each.Name == property.Name);

            if(flag != null) {
                switch(flag.Type) {
                    case ValueTypes.GameObject:
                        property.GameObjectValue = EditorGUILayout.ObjectField(flag.Name, property.GameObjectValue, typeof(GameObject), true) as GameObject;
                        break;
                    case ValueTypes.Integer:
                        property.IntValue = EditorGUILayout.IntField(flag.Name, property.IntValue);
                        break;
                    case ValueTypes.String:
                        property.StringValue = EditorGUILayout.TextField(flag.Name, property.StringValue);
                        break;
                    case ValueTypes.Sprite:
                        property.SpriteValue = EditorGUILayout.ObjectField(flag.Name, property.SpriteValue, typeof(Sprite), true) as Sprite;
                        break;
                }
            }

            return property;
        }
#endif
    }
}