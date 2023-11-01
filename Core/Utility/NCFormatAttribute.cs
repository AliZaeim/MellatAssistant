using Core.Services.Interfaces;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utility
{
    public class NCFormatAttribute : ValidationAttribute, IClientModelValidator
    {
        public bool Dataval { get; set; }
        public NCFormatAttribute(bool dataval, string errorMessage = "")
        {
            Dataval = dataval;
            ErrorMessage = errorMessage;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            if (Dataval)
            {
                context.Attributes.Add("data-val", "true");
            }            
            context.Attributes.Add("data-val-nCFormat", ErrorMessage);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            
            if (value != null)
            {
                string NC = value as string;
                if (!string.IsNullOrEmpty(NC))
                {
                    if (NC.Length == 10)
                    {
                        if (!Applications.IsValidNC(NC))
                        {
                            return new ValidationResult("کد ملی وارد شده نامعتبر است !");
                        }
                    }
                }               
            }
            return ValidationResult.Success;
        }
    }
}
