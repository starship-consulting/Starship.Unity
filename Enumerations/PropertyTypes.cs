namespace Starship.Unity.Enumerations {
    public enum PropertyTypes {

        Unknown = 0,

        // Core stats (max is 20)
        Vigor = 1,
        Agility = 2,
        Wisdom = 3,

        // Resource stats (max is 5 * core stat)
        Health = 50, // Death if it reaches 0, based on vigor
        Stamina = 51, // Capacity to perform actions, based on agility
        Focus = 52, // Max number of active abilities, based on wisdom

        // Resist stats (max is 100)
        Cold = 100,
        Heat = 101,
        Electric = 102,
        Bio = 103,
        Magic = 104,
        Pierce = 105,
        Slash = 106,
        Crush = 107
    }
}