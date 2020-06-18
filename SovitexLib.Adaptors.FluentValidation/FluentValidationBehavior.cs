using FluentValidation;
using SovitexLib.Core.Smart;

namespace SovitexLib.Adaptors.FluentValidation
{
    public class FluentValidationBehavior<TValidator> : ISmartClassBehavior where TValidator : IValidator
    {
        private readonly TValidator _validator;

        public FluentValidationBehavior(TValidator validator)
        {
            _validator = validator;
        }
        
        public void AfterInvocation(object target)
        {
            var validationResult = _validator.Validate(target);
            if (!validationResult.IsValid)
            {
                throw new SmartException($"Validation failed. {string.Join(",",validationResult.Errors)}");
            }
        }
    }
}