using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Utility
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
        AllowMultiple = false)]
    public class FilesizeAttribute : ValidationAttribute, IClientModelValidator
    {
        public float FileSizeByMB { get; set; }
        public bool DataVal { get; set; }
        public FilesizeAttribute(float fileSizeByMB, bool dataVal)
        {
            FileSizeByMB = fileSizeByMB;
            DataVal = dataVal;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            if (DataVal)
            {
                context.Attributes.Add("data-val", "true");
            }
            context.Attributes.Add("data-val-filesize", ErrorMessage);
            context.Attributes.Add("data-val-filesize-max", FileSizeByMB.ToString() + "مگا بایت ");
        }



        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            IFormFile file = value as IFormFile;
            if (file != null)
            {
                if (file.Length > FileSizeByMB * 1024 * 1024)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            return ValidationResult.Success;
        }

    }
}
