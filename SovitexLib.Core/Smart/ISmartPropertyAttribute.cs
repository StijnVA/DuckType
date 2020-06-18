using SovitexLib.Internals;

namespace SovitexLib.Core.Smart
{
    public interface ISmartPropertyAttribute<TProperty> : ISmartAttribute<ISmartPropertyBehavior<TProperty>>
    {
    }

    public interface ISmartAttribute
    {
        ISmartBehavior GetBehavior(IResolver resolver);
    }
}