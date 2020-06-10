namespace SovitexLib.Core.Smart
{
    public interface ISmartPropertyBehavior<TProperty>
    {
        void BeforeSetValue(TProperty value);
    }
}