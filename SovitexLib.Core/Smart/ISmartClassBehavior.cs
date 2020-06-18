namespace SovitexLib.Core.Smart
{
    public interface ISmartClassBehavior : ISmartBehavior
    {
        void AfterInvocation(object target);
    }
}