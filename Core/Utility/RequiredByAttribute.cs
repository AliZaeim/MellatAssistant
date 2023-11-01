using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utility
{
    public class RequiredByAttribute : ValidationAttribute, IClientModelValidator
    {
        RequiredAttribute _innerAttribute = new RequiredAttribute();
        public string _dependentProperty { get; set; }
        public object _targetValue { get; set; }
        public bool DataVal { get; set; }

        public RequiredByAttribute(string dependentProperty, object targetValue, bool dataVal)
        {
            this._dependentProperty = dependentProperty;
            this._targetValue = targetValue;
            DataVal = dataVal;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var field = validationContext.ObjectType.GetProperty(_dependentProperty);
            if (field != null)
            {
                var dependentValue = field.GetValue(validationContext.ObjectInstance, null);
                if ((dependentValue == null && _targetValue == null) || (dependentValue.Equals(_targetValue)))
                {
                    if (!_innerAttribute.IsValid(value))
                    {
                        string name = validationContext.DisplayName;
                        string specificErrorMessage = ErrorMessage;
                        if (specificErrorMessage.Length < 1)
                            specificErrorMessage = ErrorMessage;

                        return new ValidationResult(specificErrorMessage, new[] { validationContext.MemberName });
                    }
                }
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(FormatErrorMessage(_dependentProperty));
            }
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            if (DataVal)
            {
                context.Attributes.Add("data-val", "true");
            }
            context.Attributes.Add("data-val-requiredby", ErrorMessage);
            context.Attributes.Add("data-val-requiredby-otherproperty", _dependentProperty);
            context.Attributes.Add("data-val-requiredby-otherpropertyvalue", _targetValue.ToString());
        }
    }
}
