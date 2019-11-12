using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CameraSamples.Views
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
            PhotoOneButton.Clicked += (s, e) =>
                {
                    ((App)App.Current).OnShouldTakePicture();

                  ((App)App.Current).SetImagePath = (value) =>
                    {
                        PhotoOneImage.Source = value;
                    };
                };
        }
    }
}
