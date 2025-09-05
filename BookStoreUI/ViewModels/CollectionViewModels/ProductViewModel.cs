using BookStoreUI.ViewModels.BaseViewModels;

namespace BookStoreUI.ViewModels.CollectionViewModels
{
    public class ProductViewModel : ViewModelsBase
    {
        private int _productId;
        public int ProductId
        {
            get
            {
                return _productId;
            }
            set
            {
                _productId = value;
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _authorFullName;
        public string AuthorFullName
        {
            get
            {
                return _authorFullName;
            }
            set
            {
                _authorFullName = value;
                OnPropertyChanged(nameof(AuthorFullName));
            }
        }

        private string _producerName;
        public string ProducerName
        {
            get
            {
                return _producerName;
            }
            set
            {
                _producerName = value;
                OnPropertyChanged(nameof(ProducerName));
            }
        }

        private short _pagesAmount;
        public short PagesAmount
        {
            get
            {
                return _pagesAmount;
            }
            set
            {
                _pagesAmount = value;
                OnPropertyChanged(nameof(PagesAmount));
            }
        }

        private string _genre;
        public string Genre
        {
            get
            {
                return _genre;
            }
            set
            {
                _genre = value;
                OnPropertyChanged(nameof(Genre));
            }
        }

        private short _year;
        public short Year
        {
            get
            {
                return _year;
            }
            set
            {
                _year = value;
                OnPropertyChanged(nameof(Year));
            }
        }

        private string _isContinuation;
        public string IsContinuation
        {
            get
            {
                return _isContinuation;
            }
            set
            {
                _isContinuation = value;
                OnPropertyChanged(nameof(IsContinuation));
            }
        }

        private int _amountInStock;
        public int AmountInStock
        {
            get
            {
                return _amountInStock;
            }
            set
            {
                _amountInStock = value;
                OnPropertyChanged(nameof(AmountInStock));
            }
        }

        private int _cost;
        public int Cost
        {
            get
            {
                return _cost;
            }
            set
            {
                _cost = value;
                OnPropertyChanged(nameof(Cost));
            }
        }

        private int _price;
        public int Price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }
    }
}