using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace PickerSamples
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            this.BindingContext = new MainPageViewModel();
            
            SampleTwoMethod();
            SampleThreeMethod();
        }

        private void SampleTwoMethod()
        {
            Dictionary<string, Color> nameToColor = new Dictionary<string, Color>
            {
                { "Aqua", Color.Aqua }, { "Black", Color.Black },
                { "Blue", Color.Blue }, { "Fuschia", Color.Fuschia },
                { "Gray", Color.Gray }, { "Green", Color.Green },
                { "Lime", Color.Lime }, { "Maroon", Color.Maroon },
                { "Navy", Color.Navy }, { "Olive", Color.Olive },
                { "Purple", Color.Purple }, { "Red", Color.Red },
                { "Silver", Color.Silver }, { "Teal", Color.Teal },
                { "White", Color.White }, { "Yellow", Color.Yellow }
            };

            foreach (string colorName in nameToColor.Keys)
            {
                SampleTwo.Items.Add(colorName);
            }

            SampleTwo.SelectedIndexChanged += (sender, args) =>
            {
                if (SampleTwo.SelectedIndex == -1)
                {
                    SampleTwoBoxView.Color = Color.Default;
                }
                else
                {
                    string colorName = SampleTwo.Items[SampleTwo.SelectedIndex];
                    SampleTwoBoxView.Color = nameToColor[colorName];
                }
            };
        }

        private void SampleThreeMethod()
        {
            
        }
    }

    public class MainPageViewModel
    {
        public MainPageViewModel()
        {
            this.Projects = new[]
            {
                "BEE", "NEW", "POI", "REG", "SWC", "WAR"
            };
        }

        public IEnumerable<string> Projects { get; private set; }
    }
}
