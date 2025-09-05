using BookStoreUI.ViewModels.BaseViewModels;

namespace BookStoreUI.ViewModels.CollectionViewModels
{
    public class DiscountViewModel : ViewModelsBase
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

        private string _discountName;
        public string DiscountName
        {
            get
            {
                return _discountName;
            }
            set
            {
                _discountName = value;
            }
        }

        private byte _interest;
        public byte Interest
        {
            get
            {
                return _interest;
            }
            set
            {
                _interest = value;
            }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                _startDate = value;
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                _endDate = value;
            }
        }
    }
}