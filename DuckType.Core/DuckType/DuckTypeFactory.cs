﻿using System;
using Castle.DynamicProxy;

namespace DuckType.Core.DuckType
{
    public class DuckTypeFactory
    {
        public T CreateDuckedTypeObject<T>(object original, Action<DuckTypeOptions> options = null) where T : class
        {
            var duckTypeOptions = new DuckTypeOptions();
            options?.Invoke(duckTypeOptions);
            var proxyGenerator = new ProxyGenerator();
            return proxyGenerator.CreateInterfaceProxyWithoutTarget<T>(new DuckTypingInterceptor<T>(original, duckTypeOptions));
        }
    }
}