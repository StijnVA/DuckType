using DuckType.Behaviors;

namespace DuckType.Attributes;

public class BetweenBoundariesIntAttribute(
    int min,
    int max,
    CompensationBehavior compensationBehavior = CompensationBehavior.ThrowException)
    : BetweenBoundariesAttribute<int>(min, max, compensationBehavior);