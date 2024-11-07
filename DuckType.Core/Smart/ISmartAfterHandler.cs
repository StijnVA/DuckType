using Castle.DynamicProxy;

namespace DuckType.Core.Smart
{
    public interface ISmartAfterHandler : ISmartHandler
    {
      
        void HandleAfter<TEntity>(IInvocation invocation, SmartContext smartContext, TEntity entity);
    }
}