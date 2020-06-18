using System;
using SovitexLib.Core.Smart.Behaviors;
using SovitexLib.Internals;

namespace SovitexLib.Core.Smart.Attributes
{
    public class SmartVampireAttribute : Attribute, ISmartActionAttribute
    {
        public ISmartActionBehavior GetBehavior(IResolver resolver)
        {
            return new VampireBehavior(resolver.Resolve<IDayNightProvider>());
        }

        ISmartBehavior ISmartAttribute.GetBehavior(IResolver resolver)
        {
            return GetBehavior(resolver);
        }
    }
    
   
}