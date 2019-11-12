using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin_Samples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Essentials_SecureStorageView : ContentPage
    {
        public Essentials_SecureStorageView()
        {
            InitializeComponent();
        }

        private async void btnStore_Clicked(object sender, EventArgs e)
        {
            try
            {
                await SecureStorage.SetAsync("oauth_token", "secret-oauth-token-value");
                var oauthToken = await SecureStorage.GetAsync("oauth_token");
                lblInfo.Text = oauthToken;

                SecureStorage.Remove("oauth_token");
                SecureStorage.RemoveAll();
            }
            catch (Exception ex)
            {
                // Possible that device doesn't support secure storage on device.
                lblInfo.Text = ex.Message;
            }
        }
    }
}