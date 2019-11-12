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
    public partial class EssentialsSmsView : ContentPage
    {
        public EssentialsSmsView()
        {
            InitializeComponent();
        }

        private async void btnSend_Clicked(object sender, EventArgs e)
        {
            try
            {
                var message = new SmsMessage("Hi there!!!", new[] { "043300333" });
                await Sms.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException ex)
            {
                // Sms is not supported on this device.
                lblTitle.Text = ex.Message;
            }
            catch (Exception ex)
            {
                // Other error has occurred.
                lblTitle.Text = ex.Message;
            }
        }
    }
}