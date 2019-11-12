using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin_Samples.Interfaces
{
    public interface IEmailService
    {
        void CreateEmail(List<string> emailAddresses, List<string> ccs, string subject, string body, string htmlBody);
    }
}
