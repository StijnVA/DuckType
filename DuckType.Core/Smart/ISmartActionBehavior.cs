using System.Reflection;

namespace DuckType.Core.Smart
{
    public interface ISmartActionBehavior : ISmartBehavior
    {
        void BeforeInvocation(MethodInfo invocationMethod);
    }
}