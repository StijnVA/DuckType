using Castle.DynamicProxy;

namespace DuckType.Core.Smart
{
    public interface ISmartBeforeHandler : ISmartHandler
    {
        void HandleBefore(IInvocation invocation, SmartContext smartContext, object entity);
    }
}