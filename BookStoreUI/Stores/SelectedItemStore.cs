using BookStoreUI.ViewModels.CollectionViewModels;
using System.ComponentModel;

namespace BookStoreUI.Stores
{
    public class SelectedItemStore : INotifyPropertyChanged
    {
        private ProductViewModel _selectedProduct;
        public ProductViewModel SelectedProduct
        {
            get
            {
                return _selectedProduct;
            }
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        public SelectedItemStore()
        {
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}