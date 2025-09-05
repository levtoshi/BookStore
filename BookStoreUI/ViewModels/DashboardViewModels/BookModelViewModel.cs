using BLL.Services.BookModelServices;
using BookStoreUI.Commands.BaseCommands;
using BookStoreUI.Commands.DashboardCommands.BookModelCommands;
using BookStoreUI.Interfaces;
using BookStoreUI.Navigation.Services.MainNavigationServices;
using BookStoreUI.Stores;
using BookStoreUI.Stores.ControlContextStores;
using BookStoreUI.ViewModels.BaseViewModels;
using BookStoreUI.ViewModels.CollectionViewModels;
using BookStoreUI.ViewModels.OtherViewModels;
using System.ComponentModel;
using System.Windows.Input;

namespace BookStoreUI.ViewModels.DashboardViewModels
{
    public class BookModelViewModel : RightPanelBase, ISubscribable
    {
        private readonly SelectedItemStore _selectedItemStore;
        private readonly IBookModelService _bookModelService;

        private ProductViewModel _selectedBook => _selectedItemStore.SelectedProduct;
        public ProductViewModel SelectedBook
        {
            get
            {
                return _selectedBook;
            }
        }

        public ICommand AddNewBookCommand { get; }
        public ICommand UpdateBookCommand { get; }
        public ICommand DeleteBookCommand { get; }
        public ICommand AddDiscountCommand { get; }


        public BookModelViewModel(IMainNavigationService<ChangeBookModelViewModel> navigateToChangeBookModelViewModel,
            IMainNavigationService<AddDiscountViewModel> navigateToAddDiscountViewModel,
            IBookModelService bookModelService,
            ChangeBookModelControlContextStore changeBookModelControlContextStore,
            SelectedItemStore selectedItemStore)
        {
            _selectedItemStore = selectedItemStore;

            _bookModelService = bookModelService;

            AddNewBookCommand = new RelayCommand((object? s) =>
            {
                changeBookModelControlContextStore.ChangeBookModelControlContextObject.IsUpdateMode = false;
                navigateToChangeBookModelViewModel.Navigate();
            });

            UpdateBookCommand = new RelayCommand(
                (object? s) =>
                {
                    changeBookModelControlContextStore.ChangeBookModelControlContextObject.IsUpdateMode = true;
                    navigateToChangeBookModelViewModel.Navigate();
                },
                (object? s) => SelectedBook != null);

            DeleteBookCommand = new DeleteBookModelCommand(this, _bookModelService, _selectedItemStore);

            AddDiscountCommand = new RelayCommand(
                (object? s) =>
                {
                    navigateToAddDiscountViewModel.Navigate();
                },
                (object? s) => SelectedBook != null);
        }

        public void SubscribeToEvents()
        {
            _selectedItemStore.PropertyChanged += OnSelectedItemPropertyChanged;
            if (DeleteBookCommand is ISubscribable subscribable)
            {
                subscribable.SubscribeToEvents();
            }
        }

        private void OnSelectedItemPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedItemStore.SelectedProduct))
            {
                (UpdateBookCommand as RelayCommand)?.OnCanExecutedChanged();
                (AddDiscountCommand as RelayCommand)?.OnCanExecutedChanged();
            }
        }

        public override async Task SetCollectionToDefault()
        {
            await _bookModelService.SetToDefault();
        }

        public override void Dispose()
        {
            if (DeleteBookCommand is IDisposable disposable)
            {
                disposable.Dispose();
            }
            _selectedItemStore.PropertyChanged -= OnSelectedItemPropertyChanged;
            base.Dispose();
        }
    }
}