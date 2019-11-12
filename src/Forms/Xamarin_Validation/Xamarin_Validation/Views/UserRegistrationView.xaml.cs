using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Validation.Validators;
using Xamarin_Validation.ViewModels;

namespace Xamarin_Validation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserRegistrationView : ContentPage
    {
        public UserRegistrationView()
        {
            InitializeComponent();

            BindingContext = new UserRegistrationViewModel(new ValidatorRegistry());
        }
    }
}