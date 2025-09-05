using BLL.Services.BookStockServices;
using BookStoreUI.Commands.DashboardCommands.BookStockCommands;
using BookStoreUI.Interfaces;
using BookStoreUI.Navigation.Services.DashboardNavigationServices;
using BookStoreUI.Stores;
using BookStoreUI.Stores.ControlContextStores;
using BookStoreUI.ViewModels.BaseViewModels;
using System.Collections;
using System.ComponentModel;
using System.Windows.Input;

namespace BookStoreUI.ViewModels.DashboardViewModels
{
    public class ChangeBookStockViewModel : RightPanelBase, INotifyDataErrorInfo, ISubscribable
    {
        private int _amount;
        public int Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));

                ClearErrors(nameof(Amount));

                if (!HasAmountGreaterThanZero)
                {
                    AddError("Amount must be greater than zero.", nameof(Amount));
                }

                OnPropertyChanged(nameof(CanChangeBookStock));
            }
        }

        public bool CanChangeBookStock =>
            HasAmountGreaterThanZero &&
            !HasErrors;
        public bool HasAmountGreaterThanZero => Amount > 0;

        private readonly Dictionary<string, List<string>> _propertyNameToErrorsDictionary;
        public bool HasErrors => _propertyNameToErrorsDictionary.Any();
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public bool IsWriteOffMode { get; }

        public ICommand SubmitCommand { get; set; }

        public ChangeBookStockViewModel(IDashboardNavigationService<BookStockViewModel> navigateToBookStockViewModelService,
            IBookStockService bookStockService,
            ChangeBookStockControlContextStore changeBookStockControlContextStore,
            SelectedItemStore selectedItemStore)
        {
            IsWriteOffMode = changeBookStockControlContextStore.ChangeBookStockControlContextObject.IsWriteOffMode;

            _propertyNameToErrorsDictionary = new Dictionary<string, List<string>>();

            SubmitCommand = IsWriteOffMode ?
                new WriteOffBookCommand(this, navigateToBookStockViewModelService, bookStockService, selectedItemStore.SelectedProduct) :
                new AddBookStockCommand(this, navigateToBookStockViewModelService, bookStockService, selectedItemStore.SelectedProduct);
        }

        public void SubscribeToEvents()
        {
            if (SubmitCommand is ISubscribable subscribable)
            {
                subscribable.SubscribeToEvents();
            }
        }

        public IEnumerable GetErrors(string propertyName)
        {
            return _propertyNameToErrorsDictionary.GetValueOrDefault(propertyName, new List<string>());
        }

        private void AddError(string errorMessage, string propertyName)
        {
            if (!_propertyNameToErrorsDictionary.ContainsKey(propertyName))
            {
                _propertyNameToErrorsDictionary.Add(propertyName, new List<string>());
            }

            _propertyNameToErrorsDictionary[propertyName].Add(errorMessage);

            OnErrorsChanged(propertyName);
        }

        private void ClearErrors(string propertyName)
        {
            _propertyNameToErrorsDictionary.Remove(propertyName);

            OnErrorsChanged(propertyName);
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public override void Dispose()
        {
            if (SubmitCommand is IDisposable disposable)
            {
                disposable.Dispose();
            }

            base.Dispose();
        }
    }
}