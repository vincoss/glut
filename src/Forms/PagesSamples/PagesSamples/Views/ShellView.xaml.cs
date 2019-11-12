using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PagesSamples.Views
{
    public partial class ShellView : ContentPage
    {
        public ShellView()
        {
            InitializeComponent();

            this.BindingContext = new ShellViewModel();
            PageListView.ItemSelected += PageListView_ItemSelected;
        }

        private void PageListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as KeyData;

            if (item != null)
            {
                this.Title = item.Value;

                switch (item.Key)
                {
                    case "ContentPage":
                        {
                            App.Current.MainPage = new NavigationPage(new ContentPageSample());
                            break;
                        }
                    case "MasterPage":
                        {
                            App.Current.MainPage = new NavigationPage(new MasterPage());
                            break;
                        }
                    case "TabbedPageOne":
                        {
                            App.Current.MainPage = new NavigationPage(new TabbedPageSampleOne());
                            break;
                        }
                    case "TabbedPageTwo":
                        {
                            App.Current.MainPage = new NavigationPage(new TabbedPageSampleTwo());
                            break;
                        }
                    case "TabbedPageThree":
                        {
                            App.Current.MainPage = new NavigationPage(new TabbedPageSampleThree());
                            break;
                        }
                    case "CarouselPageOne":
                        {
                            App.Current.MainPage = new NavigationPage(new CarouselPageSampleOne());
                            break;
                        }
                    case "CarouselPageTwo":
                        {
                            App.Current.MainPage = new NavigationPage(new CarouselPageSampleTwo());
                            break;
                        }
                    case "NavigationSampleOne":
                        {
                            App.Current.MainPage = new NavigationPage(new NavigationPageSample());
                            break;
                        }
                }
            }
        }
    }

    public class ShellViewModel : INotifyPropertyChanged
    {
        public ShellViewModel()
        {
            this.Views = new List<KeyData>
            {
                new KeyData { Key = "ContentPage", Value = "Content Page"},
                new KeyData { Key = "MasterPage", Value = "Master Page"},
                new KeyData { Key = "TabbedPageOne", Value = "Tabbed Page One"},
                new KeyData { Key = "TabbedPageTwo", Value = "Tabbed Page Two"},
                new KeyData { Key = "TabbedPageThree", Value = "Tabbed Page Three"},
                new KeyData { Key = "CarouselPageOne", Value = "Carousel Page One"},
                new KeyData { Key = "CarouselPageTwo", Value = "Carousel Page Two"},
                new KeyData { Key = "NavigationSampleOne", Value = "Navigation Sample One"}
            };
        }

        public IEnumerable<KeyData> Views { get; private set; }

        #region INotifyPropertyChanged implementation

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged; 

        #endregion
    }

    public class KeyData
    {
        public string Key { get; set;}
        public string Value { get; set;}
    }
}
