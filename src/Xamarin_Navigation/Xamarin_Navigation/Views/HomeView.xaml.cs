using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin_Navigation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : ContentPage
    {
        public HomeView()
        {
            InitializeComponent();

            List<Page> pages = new List<Page>();
            pages.Add(new SettingView());
            pages.Add(new TabbedPageOneView());
            pages.Add(new TabbedPageTemplateView());

            pages.Add(new PageOneView(DateTime.Now));
            pages.Add(new PageTwoView());

            pages.Add(new ModalPageView());
            pages.Add(new ModalPageParentView());

            pages.Add(new MasterDetailPageView());

            pages.Add(new ContentPageOneView());

            pages.Add(new CarouselPageView());
            pages.Add(new CarouselPageTemplateView());

            ListOfPages.ItemsSource = pages;
        }

        async void itemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var page = (Page)e.SelectedItem;

                // Modals
                if (page.GetType() == typeof(ModalPageView))
                {
                    await this.Navigation.PushModalAsync((Page)e.SelectedItem);
                }
                else
                {
                    await this.Navigation.PushAsync((Page)e.SelectedItem);
                }
            }
            ListOfPages.SelectedItem = null;
        }
    }
}