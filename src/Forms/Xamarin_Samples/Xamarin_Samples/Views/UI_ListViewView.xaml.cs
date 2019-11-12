using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Samples.ViewModels;

namespace Xamarin_Samples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UI_ListViewView : ContentPage
    {
        public UI_ListViewView()
        {
            InitializeComponent();

            var model = new UI_ListViewViewModel();

            listViewCustom.ItemsSource = model.Fruits;

            BindingContext = model;
        }

        public class UI_ListViewViewModel : BaseViewModel
        {
            public UI_ListViewViewModel()
            {
                Fruits = new[]
                {
                    new FruitInfo {Title = "Apple", Subtitle = "Royal Gala", ImageUrl = ""},
                    new FruitInfo {Title = "Kiwi", Subtitle = "Nice green", ImageUrl = ""},
                };
            }

            FruitInfo _selectedFruit;
            public FruitInfo SelectedFruit
            {
                get { return _selectedFruit; }
                set { SetProperty(ref _selectedFruit, value); }
            }

            public IEnumerable<FruitInfo> Fruits { get; set; }
        }

        public class FruitInfo
        {
            public string Title { get; set; }
            public string Subtitle { get; set; }
            public string ImageUrl { get; set; }
        }
    }
}