using Accounting.Pages;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace Accounting.Utilities
{
    public class Required : ValidationAttribute 
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var LRes = (IStringLocalizer<Resource>)validationContext.GetService(typeof(IStringLocalizer<Resource>));
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return new ValidationResult(LRes[$"{validationContext.MemberName}"] + " " + LRes["is required"]);
            }
            return ValidationResult.Success;
        }
    }

    public class PhoneNumber : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var LRes = (IStringLocalizer<Resource>)validationContext.GetService(typeof(IStringLocalizer<Resource>));
            if(value == null || value.ToString().Length != 10)
            {
                return new ValidationResult(LRes["PhoneNumber"] + " " + LRes["must have 10 digits"]);
            }
            if(!int.TryParse(value.ToString(), out var phoneNumber))
            {
                return new ValidationResult(LRes["PhoneNumber"] + " " + LRes["cannot contains alphabet digit(s)"]);
            }
            return ValidationResult.Success;
        }
    }
}
