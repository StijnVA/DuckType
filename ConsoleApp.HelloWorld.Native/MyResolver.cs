using System;
using ConsoleApp.Lib;
using DuckType.Core.Smart;
using DuckType.Core.Smart.Behaviors;
using DuckType.Internals;

namespace ConsoleApp.HelloWorld.Native
{
    internal class MyResolver : IResolver
    {
        private readonly DefaultResolver _defaultResolver = new DefaultResolver();
        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        public object Resolve(Type type)
        {
            if (type == typeof(IDayNightProvider))
                return new DayNightProvider();
            
            return _defaultResolver.Resolve(type);
        }
    }
}