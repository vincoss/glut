﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin_Samples.Services;

namespace Xamarin_Samples.ViewModels
{
    public class ListViewInfiniteScrollViewModel : BaseViewModel
    {
        private const int PageSize = 10;
        private readonly DataService dataService = new DataService();

        public ListViewInfiniteScrollViewModel()
        {
            SearchResults = new ObservableCollection<string>();
            RefreshCommand = new Command(OnRefreshCommand);
        }

        public async void Initialize()
        {
           await DownloadDataAsync();
        }

        private async Task DownloadDataAsync()
        {
            try
            {
                IsBusy = true;
                var pageIndex = SearchResults.Count / PageSize;
                var items = await dataService.GetSearchResults(pageIndex, PageSize);
                foreach (var item in items)
                {
                    SearchResults.Add(item);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void OnRefreshCommand()
        {
            await DownloadDataAsync();
        }

        public ICommand RefreshCommand { get; private set; }

        private ObservableCollection<string> _searchResults;

        public ObservableCollection<string> SearchResults
        {
            get { return _searchResults; }
            set { SetProperty(ref _searchResults, value); }
        }
    }
}
