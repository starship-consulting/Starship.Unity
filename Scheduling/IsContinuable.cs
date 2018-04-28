using System;

namespace Assets.Scripts.Scheduling {
    public interface IsContinuable {
        Promise Then(Action action);
    }
}