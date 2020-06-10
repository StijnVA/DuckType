using Castle.DynamicProxy;

namespace SovitexLib.Core.Smart
{
    public interface ISmartHandler
    {
        void Handle(IInvocation invocation, SmartContext smartContext, object entity);
    }
}