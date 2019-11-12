using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Xamarin_Localization.Globalization
{
    public class CultureChangedMessage
    {
        public CultureChangedMessage(string languageName) : this(new CultureInfo(languageName))
        {
        }

        public CultureChangedMessage(CultureInfo newCultureInfo)
        {
            if(newCultureInfo == null)
            {
                throw new ArgumentNullException(nameof(newCultureInfo));
            }
            NewCultureInfo = newCultureInfo;
        }

        public CultureInfo NewCultureInfo { get; private set; }

    }
}
