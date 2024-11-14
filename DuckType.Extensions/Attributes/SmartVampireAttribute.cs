using System;
using DuckType.Core.Smart;
using DuckType.Core.Smart.Behaviors;
using DuckType.Extensions.Behaviors;
using DuckType.Internals;

namespace DuckType.Extensions.Attributes
{
    /// <summary>
    /// Attribute that indicates that a method can only be executed during the night.
    ///
    /// This Attribute mainly exists for demo purpose rather than having a particular use.
    /// </summary>
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