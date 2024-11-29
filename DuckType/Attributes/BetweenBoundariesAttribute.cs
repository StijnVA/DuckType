using System;
using DuckType.Behaviors;
using DuckType.Core.Smart;
using DuckType.Internals;

namespace DuckType.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class BetweenBoundariesAttribute<T>(
        T min,
        T max,
        CompensationBehavior compensationBehavior = CompensationBehavior.ThrowException)
        : Attribute, ISmartPropertyAttribute<T>
        where T : IComparable
    {
        public ISmartPropertyBehavior<T> GetBehavior(IResolver _)
        {
            return new BetweenBoundaries<T>(min, max, compensationBehavior);
        }

        ISmartBehavior ISmartAttribute.GetBehavior(IResolver resolver)
        {
            return GetBehavior(resolver);
        }
    }
}