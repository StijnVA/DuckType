using System;

namespace DuckType.Internals
{
    public interface IResolver
    {
        T Resolve<T>();
        object Resolve(Type type);
    }
}