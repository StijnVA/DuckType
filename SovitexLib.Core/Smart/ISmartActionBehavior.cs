using System.Reflection;

namespace SovitexLib.Core.Smart
{
    public interface ISmartActionBehavior : ISmartBehavior
    {
        void BeforeInvocation(MethodInfo invocationMethod);
    }
}