using Assets.Scripts.Controls;
using Assets.Scripts.Core;
using Assets.Scripts.EventHandling.Events;
using Assets.Scripts.Events;
using DunGen;
using UnityEngine;

namespace Assets.Scripts.Components {
    public class RandomDungeonGenerator : BaseComponent {

        protected override void Awake() {
            base.Awake();
            On<KeyPressed>(OnKeyPressed);

            Dungeon = GetComponentInChildren<RuntimeDungeon>();
            Dungeon.Generator.OnGenerationStatusChanged += OnGenerationStatusChanged;
            Dungeon.Generate();
        }

        private void OnGenerationStatusChanged(DungeonGenerator generator, GenerationStatus status) {
            if (status != GenerationStatus.Complete) {
                return;
            }

            foreach (var tile in Dungeon.Generator.CurrentDungeon.AllTiles) {
                foreach (Transform child in tile.transform) {
                    if (child.name.ToLower().Contains("floor") || child.name.ToLower().Contains("base_")) {
                        child.gameObject.layer = 10;
                    }
                }

                var lights = tile.GetComponentsInChildren<Light>();

                foreach (var light in lights) {
                    if (light.name.ToLower().Contains("flickering")) {
                        light.shadows = LightShadows.Soft;
                        light.range = 8;
                        light.color = new Color(0.4f, 0.4f, 0.4f);
                    }
                    else if (light.transform.parent.name.ToLower().Contains("candlestick")) {
                        light.shadows = LightShadows.Soft;
                        light.range = 3;
                        light.color = new Color(0.6f, 0.6f, 0.6f);
                    }
                }
            }
        }


        private void OnKeyPressed(KeyPressed e) {
            if (e.KeyCode == KeyCode.R && e.Status == KeyStatuses.Up) {
                Publish(new DungeonReset());
                Dungeon.Generate();
            }
        }

        private RuntimeDungeon Dungeon { get; set; }
    }
}