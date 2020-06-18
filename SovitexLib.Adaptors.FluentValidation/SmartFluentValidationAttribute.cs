using System;
using FluentValidation;
using SovitexLib.Core.Smart;
using SovitexLib.Internals;

namespace SovitexLib.Adaptors.FluentValidation
{
    public class SmartFluentValidationAttribute : Attribute, ISmartAttribute
    {
        private readonly Type _type;

        public SmartFluentValidationAttribute(Type type)
        {
            _type = type;
        }
        public ISmartBehavior GetBehavior(IResolver resolver)
        {
            return new FluentValidationBehavior<IValidator>((IValidator) resolver.Resolve(_type));
        }
    }
}