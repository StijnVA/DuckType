using System;
using DuckType.Core.Smart.Behaviors;
using DuckType.Internals;

namespace DuckType.Core.Smart.Attributes
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