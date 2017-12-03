using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using WebApplication1.Utility;

namespace WebApplication1.DataBase.ProjectAttribute
{
    public class PasswordCompare : CompareAttribute
    {
        public PasswordCompare(string otherProperty) : base(otherProperty)
        {
            
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(OtherProperty);

            if (property == null)
            {
                return new ValidationResult(string.Format(CultureInfo.CurrentCulture, "Unknown property {0}", OtherProperty));
            }

            var otherValue = property.GetValue(validationContext.ObjectInstance, null) as string;

            if (string.Equals(EncryptionUtility.Decryption(value as string),
                    EncryptionUtility.Decryption(otherValue)))
                return null;

            return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
        }
    }
}