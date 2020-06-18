using System;

namespace SovitexLib.Internals
{
    public interface IResolver
    {
        T Resolve<T>();
        object Resolve(Type type);
    }
}