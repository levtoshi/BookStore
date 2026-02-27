using BLL.DTOs;
using BLL.Services.BookServices;
using BLL.Services.FilterBookServices;
using BookStoreUI.Commands.BaseCommands;
using BookStoreUI.Navigation.Services.MainNavigationServices;
using BookStoreUI.Stores;
using BookStoreUI.ViewModels.DashboardViewModels;
using BookStoreUI.ViewModels.OtherViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookStoreUI.Commands.DashboardCommands.BookModelCommands
{
    public class FilterBooksCommand : AsyncCommandBase
    {
        private readonly FilterBookViewModel _filterBookViewModel;
        private readonly IFilterBookService _filterService;
        private readonly ProductsStore _productsStore;

        public FilterBooksCommand(FilterBookViewModel filterBookViewModel,
            IFilterBookService filterService,
            ProductsStore productsStore)
        {
            _filterBookViewModel = filterBookViewModel;
            _filterService = filterService;
            _productsStore = productsStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _productsStore.RefreshAsync(await _filterService.GetBooksByFilterAsync(
                        new FilterInfoDTO()
                        {
                            Name = _filterBookViewModel.BookName,
                            Author = _filterBookViewModel.AuthorFullName,
                            Genre = _filterBookViewModel.Genre,
                            Order = _filterBookViewModel.SelectedFilter,
                            Period = _filterBookViewModel.SelectedPeriodFilter
                        }
                    ));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error filtering books: {ex.Message}", "Error", MessageBoxButton.OK);
            }
        }
    }
}