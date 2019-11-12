using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin_Samples.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(Xamarin_Samples.Droid.EmailService))]
namespace Xamarin_Samples.Droid
{
    public class EmailService : IEmailService
    {
        public void CreateEmail(List<string> emailAddresses, List<string> ccs, string subject, string body, string htmlBody)
        {
            var email = new Intent(Android.Content.Intent.ActionSend);

            if (emailAddresses?.Count > 0)
            {
                email.PutExtra(Android.Content.Intent.ExtraEmail, emailAddresses.ToArray());
            }

            if (ccs?.Count > 0)
            {
                email.PutExtra(Android.Content.Intent.ExtraCc, ccs.ToArray());
            }

            email.PutExtra(Android.Content.Intent.ExtraSubject, subject);

            email.PutExtra(Android.Content.Intent.ExtraText, body);

            email.PutExtra(Android.Content.Intent.ExtraHtmlText, htmlBody);



            email.SetType("message/rfc822");

            //MainActivity.SharedInstance.StartActivity(email);

        }
    }
}