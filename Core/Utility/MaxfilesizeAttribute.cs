using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Utility
{
    /// <summary>
    /// بر اساس کیلو بایت
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class MaxfilefizeAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly int _maxFileSize;// mB
        
        public MaxfilefizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
            
        }
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                if (value is IFormFile file)
                {
                    
                    long fileSizeibMbs = file.Length / 1024 * 1024;
                    if (fileSizeibMbs > _maxFileSize)
                    {
                        return new ValidationResult(GetErrorMessage(file.FileName));
                    }
                }
            }
            
            return ValidationResult.Success;       
        }

        public string GetErrorMessage(string name)
        {
            //return $"{name}'s size is out of range.Maximum allowed file size is { _maxFileSize} bytes.";
            return $"سایز تصویر بزرگتر از حد مجاز است!.";

        }

        public void AddValidation(ClientModelValidationContext context)
        {

            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-maxfilesize", $"سایز تصویر بزرگتر از حد مجاز است!");

        }


    }
    
    
}
