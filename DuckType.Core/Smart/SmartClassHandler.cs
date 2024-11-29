using Castle.DynamicProxy;

namespace DuckType.Core.Smart
{
    internal class SmartClassHandler : ISmartAfterHandler
    {
        private readonly ISmartClassBehavior _behavior;

        public SmartClassHandler(ISmartClassBehavior behavior)
        {
            _behavior = behavior;
        }

        public void HandleAfter<TEntity>(SmartContext smartContext, TEntity entity)
        {
            _behavior.AfterInvocation(entity);
        }
    }
}