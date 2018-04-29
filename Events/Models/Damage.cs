using Starship.Unity.Actors;
using Starship.Unity.Enumerations;

namespace Starship.Unity.Events.Models {
    public class Damage {

        public Damage() {
        }

        public Damage(int amount, DamageTypes type, Actor source = null) {
            Amount = amount;
            Type = type;
            Actor = source;
        }

        public void ChangeDamage(int amount) {
            Amount = amount;
        }

        public void ChangeDamage(int amount, DamageTypes type) {
            Amount = amount;
            Type = type;
        }
        
        public DamageTypes Type { get; private set; }

        public int Amount { get; private set; }

        public Actor Actor { get; set; }
    }
}