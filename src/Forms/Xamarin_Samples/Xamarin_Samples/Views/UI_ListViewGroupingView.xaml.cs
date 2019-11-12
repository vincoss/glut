using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class UI_ListViewGroupingView : ContentPage
    {
        public UI_ListViewGroupingView()
        {
            InitializeComponent();

            BindingContext = new UI_ListViewGroupingViewModel();
        }


        public class UI_ListViewGroupingViewModel : BaseViewModel
        {
            public UI_ListViewGroupingViewModel()
            {
                var groups = (from x in DataService.Fruits
                              select x.First().ToString()).Distinct().ToArray();

                var groupList = new List<GroupedFruitInfo>();

                foreach (var g in groups)
                {
                    var parent = new GroupedFruitInfo { Title = g, ShortName = g };
                    var child = from x in DataService.Fruits
                                where x.StartsWith(g, StringComparison.OrdinalIgnoreCase)
                                select new FruitInfo
                                {
                                    Title = x,
                                    Subtitle = $"{x} subtitle...",
                                    ImageUrl = "angry.png"
                                };

                    foreach(var ch in child)
                    {
                        parent.Add(ch);
                    }

                    groupList.Add(parent);
                }

                FruitGroup = groupList;
            }


            private FruitInfo _selectedFruit;

            public FruitInfo SelectedFruit
            {
                get { return _selectedFruit; }
                set { SetProperty(ref _selectedFruit, value); }
            }

            public IEnumerable<FruitInfo> Fruits { get; set; }

            public IEnumerable<GroupedFruitInfo> FruitGroup { get; set; }
        }

        public class FruitInfo
        {
            public string Title { get; set; }
            public string Subtitle { get; set; }
            public string ImageUrl { get; set; }

        }

        public class GroupedFruitInfo : ObservableCollection<FruitInfo>
        {
            public string Title { get; set; }
            public string ShortName { get; set; }
        }
    }
}