using Assets.Scripts.Core;
using Assets.Scripts.Extensions;
using Assets.Scripts.UI;
using UnityEngine.UI;

namespace Assets.Scripts.Players {
    public class PartyMemberPresenter : BaseComponent {

        protected override void Start() {
            base.Start();

            UpdateState();
        }

        //[Method(MethodDisplay.Button), Inspect]
        public void UpdateState() {
            Portrait.sprite = Member.Portrait;
            Name.text = Member.Name;

            if (BodyGauge != null) {
                BodyGauge.Value = Member.Body;
                BodyGauge.FindComponent<Text>().text = Member.Body + " Body";
                BodyGauge.UpdateState();
            }

            if (MobilityGauge != null) {
                MobilityGauge.Value = Member.Mobility;
                MobilityGauge.FindComponent<Text>().text = Member.Mobility + " Mobility";
                MobilityGauge.UpdateState();
            }

            if (MindGauge != null) {
                MindGauge.Value = Member.Mind;
                MindGauge.FindComponent<Text>().text = Member.Mind + " Mind";
                MindGauge.UpdateState();
            }
        }
        
        public PartyMember Member;

        public Image Portrait;

        public Text Name;

        public SegmentedBarPresenter BodyGauge;

        public SegmentedBarPresenter MobilityGauge;

        public SegmentedBarPresenter MindGauge;
    }
}