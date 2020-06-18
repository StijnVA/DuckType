using Castle.DynamicProxy;

namespace SovitexLib.Core.Smart
{
    public interface ISmartBeforeHandler : ISmartHandler
    {
        void HandleBefore(IInvocation invocation, SmartContext smartContext, object entity);
    }
}