using Castle.DynamicProxy;

namespace DuckType.Core.Smart
{
    public interface ISmartBeforeHandler : ISmartHandler
    {
        void HandleBefore(SmartContext smartContext, object entity);
    }
}