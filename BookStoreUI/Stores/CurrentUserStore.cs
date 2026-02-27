using BookStoreUI.ViewModels.CollectionViewModels;
using System.ComponentModel;

namespace BookStoreUI.Stores
{
    public class CurrentUserStore : INotifyPropertyChanged
    {
        private UserViewModel _currentUser;
        public UserViewModel CurrentUser
        {
            get
            {
                return _currentUser;
            }
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}