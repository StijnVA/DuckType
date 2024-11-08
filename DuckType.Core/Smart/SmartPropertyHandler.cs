﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Castle.DynamicProxy;
using DuckType.Core.Extensions;

namespace DuckType.Core.Smart
{
    public class SmartPropertyHandler<TEntity, TProperty> : ISmartBeforeHandler
    {
        private readonly ISmartPropertyBehavior<TProperty> _propertyBehavior;
        private readonly Expression<Func<TEntity, TProperty>> _propertySelector;

        public SmartPropertyHandler(ISmartPropertyBehavior<TProperty> propertyBehavior, Expression<Func<TEntity,TProperty>> propertySelector)
        {
            _propertyBehavior = propertyBehavior;
            _propertySelector = propertySelector;
        }

        public void HandleBefore(IInvocation invocation, SmartContext smartContext, object entity)
        {
            if (IsInvocationOfPropertySet(invocation))
            {
                var value = (TProperty) invocation.Arguments.Single();
                _propertyBehavior.BeforeSetValue(value);
            }
        }

        private bool IsInvocationOfPropertySet(IInvocation invocation)
        {
            var setMethod = ((PropertyInfo)((MemberExpression) _propertySelector.Body).Member).SetMethod;
            var invocationMethod = invocation.Method;
            
            return invocationMethod.IsImplementationOf(setMethod);
        }

     
    }
}