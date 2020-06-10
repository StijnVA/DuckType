using System;
using SovitexLib.Core.Smart.Behaviors;

namespace SovitexLib.Core.Smart
{
    public class SmartEmailAttribute : Attribute, ISmartPropertyAttribute<string>
    {
        public ISmartPropertyBehavior<string> GetBehavior()
        {
            return new AllowOnlyEmailAddress();
        }
    }
}