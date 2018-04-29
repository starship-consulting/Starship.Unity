using Starship.Unity.UI;

namespace Starship.Unity.Databinding {
    public interface IsDataProvider<T> {
        void Fill(DataContext<T> context);
    }
}