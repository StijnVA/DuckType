using System;
using DuckType.Core.Smart;
using DuckType.Internals;
using FluentValidation;

namespace DuckType.Adaptors.FluentValidation
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