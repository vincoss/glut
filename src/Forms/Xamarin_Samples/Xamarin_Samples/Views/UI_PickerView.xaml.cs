using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Samples.Services;
using Xamarin_Samples.ViewModels;

namespace Xamarin_Samples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UI_PickerView : ContentPage
    {
        public UI_PickerView()
        {
            InitializeComponent();

            pFruit.ItemsSource = DataService.Fruits;

            BindingContext = new UI_PickerViewModel();
        }
    }

    public class UI_PickerViewModel : BaseViewModel
    { 
        public UI_PickerViewModel()
        {
            Fruits = (from x in DataService.Fruits
                     select new Fruit { Name = x}).ToArray();

            SelectedFruit = Fruits.SingleOrDefault(x => x.Name == "Coconut");
        }

        public IEnumerable<Fruit> Fruits { get; set; }

        private Fruit _selectedFruit;
        public Fruit SelectedFruit
        {
            get { return _selectedFruit; }
            set { SetProperty(ref _selectedFruit, value); }
        }

        public class Fruit
        {
            public string Name { get; set; }
        }
    }

}