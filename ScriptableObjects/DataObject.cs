using System;
using UnityEngine;

namespace Starship.Unity.ScriptableObjects {
    public abstract class DataObject : ScriptableObject, ISerializationCallbackReceiver {

        public void Repaint() {
            /*if (OnDataChanged != null) {
                OnDataChanged();
            }*/
        }
        
        protected abstract Type GetDataType();

        public void OnBeforeSerialize() {
            var type = GetDataType();

            if (Data == null || Data.GetType() != type) {
                SerializedData = string.Empty;
                return;
            }

            //fsData serializedData;

            /*Serializer.TrySerialize(Data, out serializedData);
            var json = fsJsonPrinter.CompressedJson(serializedData);
            SerializedData = json;*/
        }

        public void OnAfterDeserialize() {
            var type = GetDataType();

            if (SerializedData.Length > 0 && type != null) {
                
               // fsData data = fsJsonParser.Parse(SerializedData);

                object deserialized = null;

                try {
                    //Serializer.TryDeserialize(data, type, ref deserialized).AssertSuccessWithoutWarnings();
                    Data = deserialized;
                }
                catch {
                    UpdateInstance(true);
                }
            }
            else {
                UpdateInstance(false);
            }
        }

        public void DataChanged() {
            UpdateInstance(false);
        }

        private void UpdateInstance(bool notifyChange) {
            var type = GetDataType();

            if (type != null && (Data == null || Data.GetType() != type)) {
                Data = Activator.CreateInstance(type);

                /*if (notifyChange && OnDataChanged != null) {
                    EventHub.Publish(new UIRefreshRequested(this));
                }*/
            }
        }
        
        public object Data;

        //public event GenericEventHandler OnDataChanged;
        
        [SerializeField, HideInInspector]
        private string SerializedData = string.Empty;

        //private readonly fsSerializer Serializer = new fsSerializer();
    }
}