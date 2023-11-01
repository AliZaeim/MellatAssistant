using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;
namespace Core.Utility
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RequiredWithAttribute : ValidationAttribute, IClientModelValidator
    {
        public string PropertyName { get; set; }
        public object Value { get; set; }
        public bool Dataval { get; set; }
        public RequiredWithAttribute(string propertyName, object value, bool dataVal)
        {
            PropertyName = propertyName;            
            Value = value;
            Dataval = dataVal;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            object instance = validationContext.ObjectInstance;
            Type type = instance.GetType();
            object proprtyvalue = type.GetProperty(PropertyName).GetValue(instance, null);
            if (proprtyvalue == Value)
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            if (Dataval)
            {
                context.Attributes.Add("data-val", "true");
            }

            string errorMessage = FormatErrorMessage(context.ModelMetadata.GetDisplayName());
            context.Attributes.Add("data-val-requiredwith", errorMessage);
            context.Attributes.Add("data-val-requiredwith-otherproperty", PropertyName);
            context.Attributes.Add("data-val-requiredwith-otherpropertyvalue", Value.ToString());
        }
    }

}
