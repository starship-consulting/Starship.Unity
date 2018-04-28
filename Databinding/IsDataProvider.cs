using System;
using Assets.Scripts.UI;

namespace Assets.Scripts.Databinding {
    public interface IsDataProvider<T> {
        void Fill(DataContext<T> context);
    }
}