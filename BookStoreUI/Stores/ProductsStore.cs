using BLL.Services.BookServices;
using BookStoreUI.ViewModels.CollectionViewModels;
using BookStoreUI.ViewModelDTOMappers;
using System.Collections.ObjectModel;
using System.ComponentModel;
using BLL.DTOs;

namespace BookStoreUI.Stores
{
    public class ProductsStore : INotifyPropertyChanged
    {
        private readonly IBookService _bookService;
        public ObservableCollection<ProductViewModel> Products { get; } = new ObservableCollection<ProductViewModel>();

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            private set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        private string _errorMessage = string.Empty;
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);
        public bool HasProducts => Products.Any();


        public event PropertyChangedEventHandler? PropertyChanged;

        public ProductsStore(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task RefreshAsync(List<ProductDTO> products = null)
        {
            if (!IsLoading)
            {
                IsLoading = true;
                ErrorMessage = string.Empty;

                try
                {
                    if (products is null)
                    {
                        products = await _bookService.GetAllBooksAsync();
                    }
                    Products.Clear();
                    foreach (var product in products)
                    {
                        Products.Add(ProductMapper.ToViewModel(product));
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage = $"Failed to load books. {ex.Message}";
                }
                finally
                {
                    IsLoading = false;
                    OnPropertyChanged(nameof(HasProducts));
                }
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}