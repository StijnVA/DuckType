using System;
using DuckType.Core.Smart;

namespace DuckType.Behaviors;

public class MaxLength(int maxLength, CompensationBehavior compensationBehavior) : ISmartPropertyBehavior<string>
{
    public void BeforeSetValue(string value, SmartContext smartContext)
    {
        if (value.Length <= maxLength)
            return;
        
        switch (compensationBehavior)
        {
            case CompensationBehavior.ThrowException:
                throw new Exception($"{smartContext.PropertyName} has a length of {value.Length} which is greater than {maxLength}.");
            case CompensationBehavior.IgnoreChange:
                smartContext.CancelInvocation();
                break;
            case CompensationBehavior.ApplyBoundary:
                smartContext.SetNewTargetValue(value[..maxLength]);
                break;
            default:
                throw new InvalidOperationException("Unsupported compensation behavior.");
        }
    }
}