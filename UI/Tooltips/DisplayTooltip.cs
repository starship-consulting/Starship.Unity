﻿using System;
using Assets.Scripts.Core;
using Assets.Scripts.Enumerations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI.Tooltips {
    public class DisplayTooltip : BaseComponent, IPointerEnterHandler, IPointerExitHandler {
        
        public void OnPointerEnter(PointerEventData e) {
            if (Enter != null) {
                Enter(new TooltipModel {
                    Type = Type,
                    Target = gameObject,
                    Icon = Icon,
                    Title = Title,
                    Description = Description
                });
            }
        }

        public void OnPointerExit(PointerEventData e) {
            if (Exit != null) {
                Exit();
            }
        }

        public PropertyTypes Type;

        public Sprite Icon;

        public string Title;

        public string Description;

        public static event Action<TooltipModel> Enter;

        public static event Action Exit;
    }
}
