using Castle.DynamicProxy;

namespace SovitexLib.Core.Smart
{
    public interface ISmartAfterHandler : ISmartHandler
    {
      
        void HandleAfter<TEntity>(IInvocation invocation, SmartContext smartContext, TEntity entity);
    }
}