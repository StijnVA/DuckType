namespace SovitexLib.Core.Smart
{
    public interface ISmartPropertyBehavior<in TProperty> : ISmartBehavior
    {
        void BeforeSetValue(TProperty value);
    }
}