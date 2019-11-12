using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace PopupsSamples.Views
{
    public partial class SampleOneView : ContentPage
    {
        public SampleOneView()
        {
            InitializeComponent();

            Init();
        }

        private void Init()
        {
            AlertOneButton.Clicked += (s, e) =>
                {
                    DisplayAlert("Basic alert", "How are you?", "Close");
                };

            AlertTwoButton.Clicked += (s, e) =>
                {
                    DisplayAlert("Delete", "Are you sure you want to delete this item?", "Yes", "No");
                };

            ActionOneButton.Clicked += (s, e) =>
                {
                    DisplayActionSheet("ActionSheet: Send to?", "Cancel", null, "Email", "Twitter", "Facebook");
                };
        }
    }
}
