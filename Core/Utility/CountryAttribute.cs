using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utility
{
    public class CountryAttribute : ValidationAttribute, IClientModelValidator
    {
        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-country", "Invalid country. Valid values are USA, UK, and India.");
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string country = value.ToString();

            if (country != "USA" && country != "UK" && country != "India")
            {
                return new ValidationResult("Invalid country.Valid values are USA, UK, and India.");
            }
            return ValidationResult.Success;
        }
    }
}
