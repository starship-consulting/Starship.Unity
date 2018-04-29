using System;

namespace Starship.Unity.Scheduling {
    public interface IsContinuable {
        Promise Then(Action action);
    }
}