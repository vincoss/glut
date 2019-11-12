using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Samples.Interfaces;

namespace Xamarin_Samples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Essentials_SendEmailSampleView : ContentPage
    {
        public Essentials_SendEmailSampleView()
        {
            InitializeComponent();
        }

        private void btnSendEmail_Clicked(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(eEmail.Text))
            {
                return;
            }
            MailtoEmail(eEmail.Text);
        }

        public async void MailtoEmail(string to)
        {
            var mail = new Uri(String.Format("mailto:{0}?subject={1}&body={2}", to, "subject", "body"));
            await Launcher.OpenAsync(mail);
        }

        public async void Sms(string phoneNo)
        {
            // Following line used to open Messages app and populate below given details  
            if (Device.RuntimePlatform == Device.iOS)
            {
                var uri = new Uri(String.Format("sms:{0}&body={1}", phoneNo, "text message"));
                await Launcher.OpenAsync(uri);
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                var uri = new Uri(String.Format("sms:{0}?body={1}", phoneNo, "text message"));
                await Launcher.OpenAsync(uri);
            }
        }

        public void SendEmailService()
        {
            var recipients = new[] { "" }.ToList();
            var ccs = new[] { "" }.ToList();
            var subject = "";
            var body = "";
            var bodyHtml = "";

            DependencyService.Get<IEmailService>().CreateEmail(recipients, ccs, subject, body, bodyHtml);
        }

    }
}