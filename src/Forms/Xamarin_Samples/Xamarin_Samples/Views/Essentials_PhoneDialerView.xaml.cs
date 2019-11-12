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
    public partial class Essentials_PhoneDialerView : ContentPage
    {
        public Essentials_PhoneDialerView()
        {
            InitializeComponent();
        }

        private void btnShow_Clicked(object sender, EventArgs e)
        {
            try
            {
                PhoneDialer.Open("0733333333");
            }
            catch (ArgumentNullException anEx)
            {
                // Number was null or white space
                lblError.Text = anEx.Message;
            }
            catch (FeatureNotSupportedException ex)
            {
                // Phone Dialer is not supported on this device.
                lblError.Text = ex.Message;
            }
            catch (Exception ex)
            {
                // Other error has occurred.
                lblError.Text = ex.Message;
            }
        }
    }
}