using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin_Validation.Validators;

namespace Xamarin_Validation.Validation
{
    public static class ValidationExtensions
    {
        public static void ToModel(this IValidator validator, ModelStateDictionary model)
        {
            if (validator == null)
            {
                throw new ArgumentNullException(nameof(validator));
            }
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            throw new NotImplementedException();
        }
    }
}