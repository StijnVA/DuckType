using System;
using DuckType.Core.Smart.Behaviors;
using DuckType.Internals;

namespace DuckType.Core.Smart.Attributes
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
}