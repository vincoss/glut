using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin_Validation.Validators
{
    public class ValidatorRegistry : IValidatorFactory
    {

        public ValidatorRegistry()
        {

        }

        public IValidator<T> GetValidator<T>()
        {
            return(IValidator<T>)GetValidator(typeof(T));
        }

        public IValidator GetValidator(Type type)
        {
            if(typeof(UserValidator) == type)
            {
                return new UserValidator();
            }
            throw new NotSupportedException(nameof(type));
        }
    }
}
