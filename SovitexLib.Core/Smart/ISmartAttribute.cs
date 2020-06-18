using SovitexLib.Internals;

namespace SovitexLib.Core.Smart
{
    public interface ISmartAttribute<out TSmartBehavior>: ISmartAttribute where TSmartBehavior:ISmartBehavior
    {
        new TSmartBehavior GetBehavior(IResolver resolver);
    }
}