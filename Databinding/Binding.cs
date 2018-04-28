using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using Assets.Scripts.Core;
using Assets.Scripts.Extensions;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Collections;

namespace Assets.Scripts.Databinding {
    public class Binding : BaseComponent {

        /*private void Awake() {
            if (Source != null) {
                if (Event != null) {
                    var e = Event.GetEvent();
                    var component = Event.GetComponent();
                    e.AddDynamicHandler(component, OnEvent);
                }
            }
        }*/

        protected override void OnEnable() {
            base.OnEnable();

            if (Source != null) {
                var member = Source.GetMember();

                if (member == null) {
                    return;
                }

                if (member.MemberType == MemberTypes.Event) {
                    var e = member as EventInfo;
                    var component = Source.GetComponent();
                    e.AddDynamicHandler(component, OnEvent);
                }
            }
        }

        private void OnEvent(object[] args) {
            //var e = args.First() as EventInfo;
            var data = args.Skip(1).First();

            Bind(data);
        }

        public void Bind(object model) {
            if (Target == null || Target.Source == null) {
                return;
            }
            
            if (model is MonoBehaviour) {
                BindedModel = (model as MonoBehaviour).gameObject;
            }
            else if (model is GameObject) {
                BindedModel = model as GameObject;
            }

            if (model == null) {
                IsBinded = false;
                return;
            }

            IsBinded = true;

            if (Source != null && Source.Source != null) {
                // Todo:  This only works in editor!
                /*var script = Source.Source as MonoScript;

                if (script != null) {
                    var value = model.ReadMember<object>(Source.MemberName);
                    SetTargetValue(value);
                }*/
            }

            /*if (Source == null && SourceField != null && SourceField.Field != null) {
                model = SourceField.Field.GetValue(model);

                if (Field != null) {
                    Field.SetValue(model);

                    if (Field.Source.GetType().Is<ManagedComponent>()) {
                        Field.Source.As<ManagedComponent>().Changed();
                    }
                }
            }*/

            var targetGameObject = Target.Source as GameObject;
            var collection = model as IEnumerable;

            if (collection != null && targetGameObject != null) {
                if (targetGameObject.IsPrefab()) {
                    foreach (var item in collection) {
                        var instance = this.Create(targetGameObject);
                        Bind(instance.gameObject, item);
                    }
                }
                /*else if (Field != null) {
                    Field.SetValue(model);

                    if (Field.Source.GetType().Is<ManagedComponent>()) {
                        Field.Source.As<ManagedComponent>().Changed();
                    }
                }*/
            }
            else {
                //BindChildren(model);
            }
        }

        private void SetTargetValue(object value) {
            if (Target != null && Target.Source != null) {
                var component = Target.GetComponent();
                var member = Target.GetMember();
                member.Set(component, value);
            }
        }

        private void Bind(GameObject target, object model) {
            //Debug.Log("Reviewing: " + child.name);
            var bindings = target.GetComponents<Binding>();

            if (bindings.Any()) {
                foreach (var binding in bindings) {
                    //Debug.Log("Binding to: " + child.name);
                    binding.Bind(model);
                }

                return;
            }

            foreach (Transform each in target.transform) {
                Bind(each.gameObject, model);
            }
        }

        private void BindChildren(object model) {
            foreach (Transform each in transform) {
                Bind(each.gameObject, model);
            }
        }

        public SerializableMember Source;
        
        public SerializableReference SourceField;

        public SerializableEvent Event;

        public SerializableMember Target;

        //public SerializableObjectField Field;

        public GameObject Template;

        //public bool TargetIsPrefab;

        [ReadOnly]
        public bool IsBinded;
        
        public GameObject BindedModel;
    }
}