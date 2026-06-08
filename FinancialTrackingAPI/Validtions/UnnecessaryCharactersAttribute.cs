using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialTrackingAPI.Validtions
{
      public class UnnecassaryCharacthersAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            if (value is string str && (str.Contains("@") || str.Contains("!")))
                return new ValidationResult("@ ve ! karakterini kullanamassın");
            return ValidationResult.Success;
        }
    }
}