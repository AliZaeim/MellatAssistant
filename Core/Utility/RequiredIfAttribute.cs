using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Utility
{
    /// <summary>
    /// Provides conditional validation based on related property value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RequiredIfAttribute : ValidationAttribute, IClientModelValidator
    {
        public string PropertyName { get; set; }
        public object Value { get; set; }
        public bool Dataval { get; set; }

        public RequiredIfAttribute(string propertyName, string errorMessage = "", bool dataVal = true)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
            Dataval = dataVal;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            object instance = validationContext.ObjectInstance;
            Type type = instance.GetType();
            object proprtyvalue = type.GetProperty(PropertyName).GetValue(instance, null);
            return proprtyvalue == null && value == null ? new ValidationResult(ErrorMessage) : ValidationResult.Success;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            if (Dataval)
            {
                context.Attributes.Add("data-val", "true");
            }
            
            var errorMessage = FormatErrorMessage(context.ModelMetadata.GetDisplayName());
            context.Attributes.Add("data-val-requiredif", errorMessage);
            context.Attributes.Add("data-val-requiredif-otherproperty", PropertyName);
            context.Attributes.Add("data-val-requiredif-otherpropertyvalue", null);
        }

        

    }
}
