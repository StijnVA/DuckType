using System;
using System.Linq.Expressions;
using System.Reflection;
using Castle.DynamicProxy;
using DuckType.Core.Extensions;

namespace DuckType.Core.Smart
{
    public class SmartActionHandler<TEntity> : ISmartBeforeHandler
    {
        //private readonly Expression<Action<TEntity>> _actionSelector;
        private readonly ISmartActionBehavior _actionBehavior;
        private readonly MethodInfo _method;

        public SmartActionHandler(Expression<Action<TEntity>> actionSelector, ISmartActionBehavior actionBehavior)
        {
            _method = ((MethodCallExpression) actionSelector.Body).Method;
            _actionBehavior = actionBehavior;
        }
        
        public SmartActionHandler(MethodInfo method, ISmartActionBehavior actionBehavior)
        {
            _method = method;
            _actionBehavior = actionBehavior;
        }

        public void HandleBefore(SmartContext smartContext, object entity)
        {
            var method = smartContext.GetInvokedMethod();
            if (method.IsImplementationOf(_method))
            {
                _actionBehavior.BeforeInvocation(method);
            }
        }
    }
}