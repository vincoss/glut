using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin_Validation.Model;
using Xamarin_Validation.Validators;
using Xamarin_Validation.Validation;


namespace Xamarin_Validation.ViewModels
{
    public class UserRegistrationViewModel : INotifyPropertyChanged
    {
        private readonly IValidatorFactory _validatorFactory;
        private readonly ModelStateDictionary _modelState = new ModelStateDictionary();

        public UserRegistrationViewModel(IValidatorFactory validatorFactory)
        {
            _validatorFactory = validatorFactory;

            RegisterCommand = new Command(RegisterValidation);
        }

        void RegisterValidation()
        {
            var userObj = new UserInfo
            {
                UserName = UserName,
                PhoneNumber = PhoneNumber,
                Email = Email,
                Password = Password,
                ConfirmPassword = ConfirmPassword,
            };

            _validatorFactory.GetValidator<UserValidator>().ToModel(_modelState);

            var validationResults = _validatorFactory.GetValidator<UserValidator>().Validate(userObj);

            if (validationResults.IsValid)
            {
                App.Current.MainPage.DisplayAlert("FluentValidation", "Validation Success..!!", "Ok");
            }
            else
            {
                App.Current.MainPage.DisplayAlert("FluentValidation", validationResults.Errors[0].ErrorMessage, "Ok");
            }
        }

        #region Properties

        string _userName;
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { SetProperty(ref _phoneNumber, value); }
        }

        string _email;
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        string _confirmPassword;
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set { SetProperty(ref _confirmPassword, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
        public ICommand RegisterCommand { get; set; }

        #endregion

        #region INotifyPropertyChanged    
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
