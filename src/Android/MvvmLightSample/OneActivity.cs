using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Helpers;


namespace MvvmLightSample
{
    [Activity(Label = "One Activity")]
    public class OneActivity : Activity
    {
        private Button _myButton;
        private MainViewModel _vm;

        public MainViewModel Vm
        {
            get { return _vm ?? (_vm = new MainViewModel()); }
        }

        public Button MyButton
        {
            get { return _myButton ?? (_myButton = FindViewById<Button>(Resource.Id.MyButton)); }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.OneView);

            this.AddBinding(() => Vm.Hello, () => MyButton.Text);

            MyButton.AddCommand("Click", Vm.IncrementCommand);
        }
    }

    public class MainViewModel : ViewModelBase
    {
        private int _index;

        public const string HelloPropertyName = "Hello";

        private string _hello = "Hello!";

        public string Hello
        {
            get { return _hello; }
            set { Set(() => Hello, ref _hello, value); }
        }

        private RelayCommand _incrementCommand;

        public RelayCommand IncrementCommand
        {
            get
            {
                return _incrementCommand
                       ?? (_incrementCommand = new RelayCommand(
                                                   () =>
                                                   {
                                                       Hello = string.Format(
                                                           "Hello! {0} click(s)", ++_index);
                                                   }));
            }
        }
    }
}