using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_Framework.Application
{
    public class FileExtensionLimitationAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly string[] _validExtensions;

        public FileExtensionLimitationAttribute(string[] validExtensions)
        {
            _validExtensions = validExtensions;
        }
        public override bool IsValid(object? value)
        {
            var file = value as IFormFile;
            if (file == null) return true;

            var fileExtention = Path.GetExtension(file.FileName);
            return _validExtensions.Contains(fileExtention);
        }
        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val-fileExtensionLimit", ErrorMessage);
        }
    }
}
