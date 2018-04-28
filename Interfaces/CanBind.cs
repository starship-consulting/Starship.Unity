namespace Assets.Scripts.Interfaces {
    public interface CanBind {
    }

    public interface CanBind<in T> : CanBind {
        void Bind(T data);
    }
}