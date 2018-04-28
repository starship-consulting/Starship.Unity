using System;

namespace Assets.Scripts.Editor.Editors {
    public interface ICustomEditor {
    }

    public interface ICustomEditor<in T> : ICustomEditor {
        void Draw(T model);
    }
}