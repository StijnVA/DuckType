namespace SovitexLib.Core.Smart
{
    public interface ISmartObject
    {
        
    }
    public interface ISmartObject<TEntity> : ISmartObject
    {
        SmartController<TEntity> SmartController { get; }
    }
}