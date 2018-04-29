using System.Linq;
using Starship.Unity.Cameras;
using Starship.Unity.Core;
using Starship.Unity.Elements;
using Starship.Unity.Events;
using Starship.Unity.Events.Elements;
using Starship.Unity.Events.Targetting;
using Starship.Unity.Extensions;
using Starship.Unity.Interaction;
using Starship.Unity.Interfaces;
using UnityEngine;

namespace Starship.Unity.UI {

    [ExecuteInEditMode]
    public class CardSelector : BaseComponent, IsTrait {

        protected override void OnEnable() {
            base.OnEnable();
            
            On<ElementAdded>(OnElementAdded);
            On<ElementUsed>(OnElementUsed);
            On<TargetSelected>(OnTargetSelected);
            On<SelectedTargetComponentChanged>(OnSelectedTargetComponentChanged);
            On<CardHovered>(OnCardHovered);
            On<CardUnhovered>(OnCardUnhovered);
        }

        protected override void Start() {
            base.Start();

            if (Template == null) {
                return;
            }

            this.ClearChildren();

            Initialize();
        }

        private void Initialize() {
            foreach (var item in Inventory.Items) {
                Add(item);
            }
        }

        public void SetFilter(AbilityFilter filter) {
            var active = filter.Filter(Inventory.Items.Select(each => each.Element).ToList());

            foreach (var presenter in GetComponentsInChildren<Card>(true)) {
                presenter.SetTarget(CurrentTarget);
                presenter.gameObject.SetActive(active.Contains(presenter.Item.Element));
            }
        }

        private void OnElementAdded(ElementAdded e) {
            var element = GetElement(e.Item.Element);

            if (element != null) {
                element.Update();
            }
            else {
                Add(e.Item);
            }
        }

        private void Add(InventoryItem item) {
            var template = this.Create(Template);
            template.Set(item);

            if (!Application.isPlaying) {
                return;
            }

            this.DeferUntilNextFixedUpdate(() => Preview(template)).Then(() => {
                ClearPreview(template);
            });
        }

        private void OnElementUsed(ElementUsed e) {
            var element = GetElement(e.Element);

            if (element != null && element.Item.Quantity <= 0) {
                element.gameObject.Delete();
                element.transform.parent = transform.parent;
            }
        }

        private Card GetElement(Element element) {
            return GetComponentsInChildren<Card>().FirstOrDefault(each => each.Item.Element == element);
        }

        private void OnSelectedTargetComponentChanged(SelectedTargetComponentChanged e) {
            SetFilter(new CurrentTargetAbilityFilter(e.Target));
        }

        private void OnTargetSelected(TargetSelected e) {
            CurrentTarget = e.Target;
            SetFilter(new CurrentTargetAbilityFilter(e.Target));
        }

        private void OnCardHovered(CardHovered e) {
            Preview(e.Card);
        }

        private void OnCardUnhovered(CardUnhovered e) {
            ClearPreview(e.Card);
        }
        
        private void Preview(Card card) {
            if (card != null && PreviewCamera != null) {
                if (card.Graphic.texture != null && !card.UseAnimatedGraphic) {
                    return;
                }

                if (card.Graphic.texture == null) {
                    card.UseAnimatedGraphic = true;
                }

                CurrentCard = card;

                var model = card.Build();
                model.transform.rotation = card.GraphicRotation;

                PreviewCamera.PreviewOutline = card.Item.Element.PreviewOutline;
                PreviewCamera.PreviewDistance = card.Item.Element.PreviewDistance;
                PreviewCamera.Preview(model, CurrentCard.Graphic);
            }
        }

        private void ClearPreview(Card card) {
            if (PreviewCamera != null && CurrentCard == card) {

                if (PreviewCamera.Target != null) {
                    CurrentCard.GraphicRotation = PreviewCamera.Target.transform.rotation;
                }

                this.DeferUntilNextFixedUpdate(() => Preview(card)).Then(() => {
                    CurrentCard = null;
                    PreviewCamera.Preview(null, null);
                });
            }
        }

        public Inventory Inventory;

        public Card Template;

        public PreviewCamera PreviewCamera;

        private Card CurrentCard { get; set; }

        private Targettable CurrentTarget { get; set; }
    }
}