using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using MessageUI;
using UIKit;
using Xamarin_Samples.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(Xamarin_Samples.iOS.EmailService))]
namespace Xamarin_Samples.iOS
{
    public class EmailService : NSObject, IEmailService, IMFMailComposeViewControllerDelegate
    {


        public void CreateEmail(List<string> emailAddresses, List<string> ccs, string subject, string body, string htmlBody)
        {
            var vc = new MFMailComposeViewController();
            vc.MailComposeDelegate = this;

            if (emailAddresses?.Count > 0)
            {
                vc.SetToRecipients(emailAddresses.ToArray());
            }

            if (ccs?.Count > 0)
            {
                vc.SetCcRecipients(ccs.ToArray());
            }

            vc.SetSubject(subject);
            vc.SetMessageBody(htmlBody, true);
            vc.Finished += (sender, e) =>
            {
                vc.DismissModalViewController(true);
            };



            UIApplication.SharedApplication.Windows[0].
                RootViewController.PresentViewController(vc, true, null);

        }


    }
}