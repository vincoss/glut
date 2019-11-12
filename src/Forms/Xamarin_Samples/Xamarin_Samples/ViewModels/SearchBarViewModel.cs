using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin_Samples.Services;

namespace Xamarin_Samples.ViewModels
{
    public class SearchBarViewModel : BaseViewModel
    {
        public SearchBarViewModel()
        {
            SearchResults = DataService.Fruits;
        }

        public ICommand PerformSearchCommand => new Command<string>((string query) =>
        {
            SearchResults = DataService.GetSearchResults(query);
        });

        private IEnumerable<string> _searchResults;

        public IEnumerable<string> SearchResults
        {
            get { return _searchResults; }
            set { SetProperty(ref _searchResults, value); }
        }
    }
}
