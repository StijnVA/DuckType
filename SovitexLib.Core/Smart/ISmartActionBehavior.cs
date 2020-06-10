using System.Reflection;

namespace SovitexLib.Core.Smart
{
    public interface ISmartActionBehavior
    {
        void BeforeInvocation(MethodInfo invocationMethod);
    }
}