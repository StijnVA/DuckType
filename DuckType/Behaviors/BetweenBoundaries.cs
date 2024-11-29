using System;
using System.Collections.Generic;
using DuckType.Core.Smart;

namespace DuckType.Behaviors
{
    public class BetweenBoundaries<T>(T min, T max, CompensationBehavior compensationBehavior)
        : ISmartPropertyBehavior<T>
        where T : IComparable
    {
        public void BeforeSetValue(T value, SmartContext smartContext)
        {
            //roughly: if (_min <= value && value <= _max)
            var isNotToSmall = min.CompareTo(value) <= 0;
            var isNotToBig = value.CompareTo(max) <= 0;
            if (isNotToSmall && isNotToBig)
                return;
            
            switch (compensationBehavior)
            {
                case CompensationBehavior.ThrowException:
                    throw new ArgumentOutOfRangeException(nameof(value), $"Value must be between {min} and {max}.");
                case CompensationBehavior.IgnoreChange:
                    smartContext.CancelInvocation();
                    break;
                case CompensationBehavior.ApplyBoundary:
                    smartContext.SetNewTargetValue(isNotToSmall ? max : min);
                    break;
                default:
                    throw new InvalidOperationException("Unsupported compensation behavior.");
            }
        }
    }
}