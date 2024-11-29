using DuckType.Behaviors;

namespace DuckType.Attributes;

public class BetweenBoundariesLongAttribute(
    long min,
    long max,
    CompensationBehavior compensationBehavior = CompensationBehavior.ThrowException)
    : BetweenBoundariesAttribute<long>(min, max, compensationBehavior);