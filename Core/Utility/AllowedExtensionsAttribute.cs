using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Core.Utility
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class AllowExtensionsAttribute : ValidationAttribute, IClientModelValidator
    {
        public bool DataVal { get; set; }
        public string Extensions { get; set; } = "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF";
        public AllowExtensionsAttribute(bool dataVal, string extensions)
        {
            DataVal = dataVal;
            Extensions = extensions;
        }
        #region Public / Protected Properties  

       
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
            context.Attributes.Add("data-val-allowExtensions", ErrorMessage);
        }

        #endregion

        #region Is valid method  

        /// <summary>  
        /// Is valid method.  
        /// </summary>  
        /// <param name="value">Value parameter</param>  
        /// <returns>Returns - true is specify extension matches.</returns>  
        public override bool IsValid(object value)
        {
            // Initialization  
            IFormFile file = value as IFormFile;
            bool isValid = true;

            // Settings.  
            List<string> allowedExtensions = Extensions.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            // Verification.  
            if (file != null)
            {
                // Initialization.  
                var fileName = file.FileName;

                // Settings.  
                isValid = allowedExtensions.Any(y => fileName.EndsWith(y));
            }

            // Info  
            return isValid;
        }

        #endregion
    }
} 

