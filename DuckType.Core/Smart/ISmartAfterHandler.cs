using Castle.DynamicProxy;

namespace DuckType.Core.Smart
{
    public interface ISmartAfterHandler : ISmartHandler
    {
      
        void HandleAfter<TEntity>(SmartContext smartContext, TEntity entity);
    }
}