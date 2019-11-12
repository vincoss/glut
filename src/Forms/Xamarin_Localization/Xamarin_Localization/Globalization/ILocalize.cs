using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Xamarin_Localization.Globalization
{
    public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();
        void SetLocale(CultureInfo ci);
    }
}
