using System;
using SovitexLib.Core.Smart.Behaviors;
using SovitexLib.Internals;

namespace SovitexLib.Core.Smart.Attributes
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