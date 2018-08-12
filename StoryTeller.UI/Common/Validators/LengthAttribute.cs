using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoryTeller.Common
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class LengthAttribute : ValidationAttribute
    {
        public string LengthPropertyName { get; private set; }

        public LengthAttribute(string lengthPropertyName)
        {
            LengthPropertyName = lengthPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var result = ValidationResult.Success;

            if (value == null)
            {
                return new ValidationResult("Chapter text required");
            }

            var lengthProperty = validationContext.ObjectType.GetProperty(LengthPropertyName).GetValue(validationContext.ObjectInstance, null);
            if (lengthProperty != null)
            {
                if (lengthProperty is int)
                {
                    var length = (int)lengthProperty;
                    if (value.ToString().Count() >= length)
                    {
                        result = new ValidationResult("Chapter length must be < " + length);
                    }

                }
            }
            return result;
        }
    }
}