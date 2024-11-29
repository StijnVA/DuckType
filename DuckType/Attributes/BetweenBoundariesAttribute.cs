using System;
using DuckType.Behaviors;
using DuckType.Core.Smart;
using DuckType.Internals;

namespace DuckType.Attributes
{
    public class BetweenBoundariesIntAttribute(
        int min,
        int max,
        CompensationBehavior compensationBehavior = CompensationBehavior.ThrowException)
        : BetweenBoundariesAttribute<int>(min, max, compensationBehavior);

    public class BetweenBoundariesLongAttribute(
        long min,
        long max,
        CompensationBehavior compensationBehavior = CompensationBehavior.ThrowException)
        : BetweenBoundariesAttribute<long>(min, max, compensationBehavior);


    [AttributeUsage(AttributeTargets.Property)]
    public abstract class BetweenBoundariesAttribute<T> : Attribute, ISmartPropertyAttribute<T> where T : IComparable
    {
        private readonly T _min;
        private readonly T _max;
        private readonly CompensationBehavior _compensationBehavior;

        public BetweenBoundariesAttribute(T min, T max,
            CompensationBehavior compensationBehavior = CompensationBehavior.ThrowException)
        {
            _min = min;
            _max = max;
            _compensationBehavior = compensationBehavior;
        }
        
        public ISmartPropertyBehavior<T> GetBehavior(IResolver _)
        {
            return new BetweenBoundaries<T>(_min, _max, _compensationBehavior);
        }

        ISmartBehavior ISmartAttribute.GetBehavior(IResolver resolver)
        {
            return GetBehavior(resolver);
        }
    }
}