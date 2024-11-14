using System;
using DuckType.Internals;

namespace DuckType.Core.Smart
{
    /// <summary>
    /// A Default Resolver, will be used when no resolver has been defined.
    /// when using IoC in your application create (or import) a Resolver bound to
    /// your DI container.
    /// </summary>
    public class DefaultResolver : IResolver
    {
        public T Resolve<T>() => Activator.CreateInstance<T>();

        public object Resolve(Type type) => Activator.CreateInstance(type);
    }
}