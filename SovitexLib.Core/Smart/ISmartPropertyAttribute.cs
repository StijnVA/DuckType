namespace SovitexLib.Core.Smart
{
    internal interface ISmartPropertyAttribute<TProperty>
    {
        // ReSharper disable once UnusedMember.Global
        ISmartPropertyBehavior<TProperty> GetBehavior();
    }
}