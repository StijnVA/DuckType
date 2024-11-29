using System;
using DuckType.Behaviors;
using DuckType.Core.Smart;
using DuckType.Internals;

namespace DuckType.Attributes
{
    public class SmartEmailAttribute : Attribute, ISmartPropertyAttribute<string>
    {
        public ISmartPropertyBehavior<string> GetBehavior(IResolver _)
        {
            return new AllowOnlyEmailAddress();
        }

        ISmartBehavior ISmartAttribute.GetBehavior(IResolver resolver)
        {
            return GetBehavior(resolver);
        }
    }

    public class MaxLengthAttribute(int maxLength, CompensationBehavior compensationBehavior = CompensationBehavior.ThrowException) : Attribute, ISmartPropertyAttribute<string>
    {

        public ISmartPropertyBehavior<string> GetBehavior(IResolver resolver)
        {
            return new MaxLength(maxLength, compensationBehavior);
        }

        ISmartBehavior ISmartAttribute.GetBehavior(IResolver resolver)
        {
            return GetBehavior(resolver);
        }
    }
}