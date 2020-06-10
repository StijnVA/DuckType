namespace SovitexLib.Internals
{
    public interface IResolver
    {
        T Resolve<T>();
    }
}