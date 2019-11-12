using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Navigation.Models;

namespace Xamarin_Navigation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModalPageParentView : ContentPage
    {
        public ModalPageParentView()
        {
            InitializeComponent();

            lvUsers.ItemsSource = new[]
            {
                new Contact { Name = "Ferdinand" },
                new Contact { Name = "Minjin" },
                new Contact { Name = "Adam" },
                new Contact { Name = "Hana" },
            };
        }

        private async void lvUsers_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (lvUsers.SelectedItem != null)
            {
                var detailPage = new ModalPageContactView();
                detailPage.BindingContext = e.SelectedItem as Contact;
                lvUsers.SelectedItem = null;
                await Navigation.PushModalAsync(detailPage);
            }
        }
    }
}