using BookStoreUI.ViewModels.BaseViewModels;

namespace BookStoreUI.ViewModels.CollectionViewModels
{
    public class DelayViewModel : ViewModelsBase
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

        private string _customerFullName;
        public string CustomerFullName
        {
            get
            {
                return _customerFullName;
            }
            set
            {
                _customerFullName = value;
                OnPropertyChanged(nameof(CustomerFullName));
            }
        }

        private string _customerEmail;
        public string CustomerEmail
        {
            get
            {
                return _customerEmail;
            }
            set
            {
                _customerEmail = value;
                OnPropertyChanged(nameof(CustomerEmail));
            }
        }

        private int _amountOfBooksDelayed;
        public int AmountOfBooksDelayed
        {
            get
            {
                return _amountOfBooksDelayed;
            }
            set
            {
                _amountOfBooksDelayed = value;
                OnPropertyChanged(nameof(AmountOfBooksDelayed));
            }
        }

        private int _totalPrice;
        public int TotalPrice
        {
            get
            {
                return _totalPrice;
            }
            set
            {
                _totalPrice = value;
                OnPropertyChanged(nameof(TotalPrice));
            }
        }
    }
}