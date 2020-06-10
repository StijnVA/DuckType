using Autofac;
using SovitexLib.Internals;

namespace Sovitex.Internals.AutofacAdaptors
{
    public class Resolver : IResolver
    {
        private readonly ILifetimeScope _lifetimeScope;

        public Resolver(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }
        public T Resolve<T>()
        {
            return _lifetimeScope.Resolve<T>();
        }
    }
}