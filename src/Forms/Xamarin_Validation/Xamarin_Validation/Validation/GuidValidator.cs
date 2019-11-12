using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin_Validation.Validation
{
    public class GuidValidator : AbstractValidator<string>
    {
        public GuidValidator(string propertyName)
        {
            RuleFor(x => x).NotEmpty().OverridePropertyName(propertyName);
        }
    }
}
