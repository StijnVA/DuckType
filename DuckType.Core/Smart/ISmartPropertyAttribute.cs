using DuckType.Internals;

namespace DuckType.Core.Smart
{
    public interface ISmartPropertyAttribute<TProperty> : ISmartAttribute<ISmartPropertyBehavior<TProperty>>
    {
    }

    public interface ISmartAttribute
    {
        ISmartBehavior GetBehavior(IResolver resolver);
    }
}