using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core;
using Assets.Scripts.EventHandling;
using Assets.Scripts.EventHandling.Events;
using Assets.Scripts.Events;
using Assets.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using Event = UnityEngine.Event;

namespace Assets.Scripts.Controls {
    public class KeyboardController : BaseComponent {

        public void OnGUI() {
            if (EventSystem.current.currentSelectedGameObject != null) {
                return;
            }

            if (Event.current.isKey) {
                if (Event.current.keyCode == KeyCode.None) {
                    return;
                }

                KeyModifiers modifier = 0;

                if (Event.current.command) {
                    modifier |= KeyModifiers.CommandWin;
                }

                if (Event.current.alt) {
                    modifier |= KeyModifiers.Alt;
                }

                if (Event.current.control) {
                    modifier |= KeyModifiers.Control;
                }

                if (Event.current.shift) {
                    modifier |= KeyModifiers.Shift;
                }

                if (Event.current.numeric) {
                    modifier |= KeyModifiers.Numeric;
                }

                if (Event.current.functionKey) {
                    modifier |= KeyModifiers.FunctionKey;
                }

                if (Event.current.capsLock) {
                    modifier |= KeyModifiers.CapsLock;
                }

                var type = Event.current.type == EventType.KeyDown ? KeyStatuses.Down : KeyStatuses.Up;
                var code = Event.current.keyCode;

                if (!Status.ContainsKey(Event.current.keyCode)) {
                    Status.Add(code, new KeyStatus {
                        KeyCode = Event.current.keyCode,
                        Modifiers = modifier,
                        Status = type
                    });
                }
                else {
                    var status = Status[code];

                    if (status.Modifiers == modifier && status.Status == type) {
                        return;
                    }

                    status.Modifiers = modifier;
                    status.Status = type;
                }

                KeyPressed(code, Event.current.type == EventType.KeyDown);
            }
        }

        private void KeyPressed(KeyCode code, bool isDown) {
            Publish(new KeyPressed(Status[code]));

            foreach (var binding in Bindings.OrderBy(each => each.HasContext == null)) {
                if (binding.Key == code) {
                    if (binding.HasContext != null) {
                        var context = binding.HasContext.GetComponent<HasContext>();

                        if (context != null) {
                            if (context.GetContext() != null) {
                                Publish(new HotkeyTriggered(binding.Type));
                                return;
                            }

                            continue;
                        }
                    }

                    if (isDown) {
                        /*if (binding.Action != null && binding.Action.GameObject != null && binding.Action.GameObject.activeInHierarchy) {
                            binding.Action.Invoke();
                        }*/

                        Publish(new HotkeyTriggered(binding.Type));
                    }

                    Publish(new HotkeyStateChanged {
                        IsUp = !isDown,
                        Hotkey = binding.Type
                    });
                }
            }

            foreach(var binding in Bindings2) {
                if(binding.Key == code) {
                    if(binding.State == ButtonStates.Down && isDown) {
                        Publish(new GameEventFired { Event = binding.Event });
                    }
                    else if(binding.State == ButtonStates.Up && !isDown) {
                        Publish(new GameEventFired { Event = binding.Event });
                    }
                }
            }
        }

        private void OnHotkeyFinished(HotkeyFinished e) {
            foreach (var binding in Bindings.Where(each => each.Type == e.Hotkey)) {
                var status = GetStatus(binding.Key);

                if (status != null && status.KeyCode == binding.Key && status.Status == KeyStatuses.Down) {
                    Publish(new HotkeyTriggered(binding.Type));
                    return;
                }
            }
        }

        public static KeyStatus GetStatus(KeyCode code) {
            return Status.ContainsKey(code) ? Status[code] : null;
        }

        public HotkeyBinding[] Bindings;

        public KeyboardBinding[] Bindings2;

        private static readonly Dictionary<KeyCode, KeyStatus> Status = new Dictionary<KeyCode, KeyStatus>();
    }
}