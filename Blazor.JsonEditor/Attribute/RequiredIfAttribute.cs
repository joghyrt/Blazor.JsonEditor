using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Blazor.JsonEditor.Attribute
{
    internal class RequiredIfAttribute : ValidationAttribute
    {
        RequiredAttribute _innerAttribute = new RequiredAttribute();
        internal string _dependentProperty { get; set; }
        internal List<object> _targetValues { get; set; }


        internal RequiredIfAttribute(string propertyName, params object[] dependents)
        {
            _dependentProperty = propertyName;
            _targetValues = dependents.ToList();
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var field = validationContext.ObjectType.GetProperty(_dependentProperty);
            if (field != null)
            {
                var dependentValue = field.GetValue(validationContext.ObjectInstance, null);
                if (_targetValues?.Any(x => x.Equals(dependentValue)) == true)
                {
                    _innerAttribute.ErrorMessage = "Required!";
                    return _innerAttribute.GetValidationResult(value, validationContext);
                }

                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(FormatErrorMessage(_dependentProperty));
            }
        }
    }
}
