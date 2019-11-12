using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace ScrollViewSamples.Converters
{
    public class StringColorToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color;
            if (value == null)
            {
                color = Color.FromRgba(0x00, 0x00, 0x00, 0xff);
            }
            else if ((string.IsNullOrEmpty(value.ToString())) || (value.ToString() == "0"))
            {
                color = Color.FromRgba(0x00, 0x00, 0x00, 0xff);
            }
            else
            {
                try
                {
                    string val = value.ToString();
                    val = val.Replace("#", "");

                    byte a = System.Convert.ToByte("ff", 16);

                    byte pos = 0;

                    if (val.Length == 8)
                    {
                        a = System.Convert.ToByte(val.Substring(pos, 2), 16);
                        pos = 2;
                    }
                    byte r = System.Convert.ToByte(val.Substring(pos, 2), 16);
                    pos += 2;
                    byte g = System.Convert.ToByte(val.Substring(pos, 2), 16);
                    pos += 2;
                    byte b = System.Convert.ToByte(val.Substring(pos, 2), 16);

                    Color col = Color.FromRgba(r, g, b, a);

                    color = col;
                }
                catch
                {
                    color = Color.FromRgba(0x00, 0x00, 0x00, 0xff);
                }
            }
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (Color)value;
            return val.ToString();
        }
    }
}
