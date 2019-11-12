using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GesturesSamples.Views
{
    public partial class TapMainView : ContentPage
    {
        private int _tapCount;

        public TapMainView()
        {
            InitializeComponent();

            this.BindingContext = new TapMainViewModel();

            CodeBehindSample();
            CodeBindingCommand();
        }

        public void CodeBehindSample()
        {
            var item = new BoxView();
            item.HeightRequest = 50;
            item.WidthRequest = 50;
            item.BackgroundColor = Color.Red;

            var tap = new TapGestureRecognizer();
            tap.NumberOfTapsRequired = 2;
            tap.Tapped += tap_Tapped;
            item.GestureRecognizers.Add(tap);

            LayoutRoot.Children.Add(item);

            // NOTE: Button tap gesture does not fire, not sure why, need to check source
            var button = new Button();
            var buttonTap = new TapGestureRecognizer(OnTap);
            buttonTap.Tapped += tap_Tapped;
            button.GestureRecognizers.Add(buttonTap);

            LayoutRoot.Children.Add(button);

        }

        public void CodeBindingCommand()
        {
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.SetBinding(TapGestureRecognizer.CommandProperty, "TapCommand");
            MvvmCommandCodeBindingBoxView.GestureRecognizers.Add(tapGestureRecognizer);
        }

        private void tap_Tapped(object sender, EventArgs e)
        {
            DetailLabel.Text = string.Format("{0}{1}{2}", DetailLabel.Text, Environment.NewLine, sender);
        }

        private void OnTap(View view)
        {
            _tapCount++;
            var button = (Button)view;

            // watch the monkey go from color to black&white!
            if (_tapCount % 2 == 0)
            {
                button.Text = "1";
            }
            else
            {
                button.Text = "2";
            }
        }

        private void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            _tapCount++;
            DetailLabel.Text = string.Format("{0}{1}{2}", DetailLabel.Text, Environment.NewLine, _tapCount);
        }
    }

    public class TapMainViewModel
    {
        private int taps = 0;
        private ICommand tapCommand;

        public TapMainViewModel()
        {
            tapCommand = new Command(OnTapped);
        }

        public ICommand TapCommand
        {
            get { return tapCommand; }
        }

        private void OnTapped(object s)
        {
            taps++;
            Debug.WriteLine("parameter: " + s);
        }
    }
}