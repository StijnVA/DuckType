using System;
using System.Collections.Generic;
using DuckType.Core.Smart;

namespace DuckType.Behaviors
{
    public  class BetweenBoundaries<T> : ISmartPropertyBehavior<T> where T : IComparable
    {
        private readonly T _min;
        private readonly T _max;
        private readonly CompensationBehavior _compensationBehavior;

        public BetweenBoundaries(T min, T max, CompensationBehavior compensationBehavior)
        {
            _min = min;
            _max = max;
            _compensationBehavior = compensationBehavior;
        }

        public void BeforeSetValue(T value, SmartContext smartContext)
        {
            //roughly: if (_min <= value && value <= _max)
            var isNotToSmall = _min.CompareTo(value) <= 0;
            if (isNotToSmall && value.CompareTo(_max) <= 0)
                return;
            
            switch (_compensationBehavior)
            {
                case CompensationBehavior.ThrowException:
                    throw new ArgumentOutOfRangeException(nameof(value), $"Value must be between {_min} and {_max}.");
                case CompensationBehavior.IgnoreChange:
                    smartContext.CancelInvocation();
                    break;
                case CompensationBehavior.ApplyBoundary:
                    smartContext.SetNewTargetValue(isNotToSmall ? _max : _min);
                    break;
                default:
                    throw new InvalidOperationException("Unsupported compensation behavior.");
            }
        }
    }
}