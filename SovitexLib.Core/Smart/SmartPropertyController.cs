using System;
using System.Linq.Expressions;
using Castle.DynamicProxy;
using SovitexLib.Core.Extensions;

namespace SovitexLib.Core.Smart
{
    public class SmartPropertyController<TEntity, TProperty>
    {
        private readonly SmartController<TEntity> _smartController;
        private readonly Expression<Func<TEntity, TProperty>> _propertySelector;

        public SmartPropertyController(SmartController<TEntity> smartController, Expression<Func<TEntity,TProperty>> propertySelector)
        {
            _smartController = smartController;
            _propertySelector = propertySelector;
        }

        public void AddBehavior(ISmartPropertyBehavior<TProperty> propertyBehavior)
        {
            _smartController.AddHandler(new SmartPropertyHandler<TEntity, TProperty>(propertyBehavior, _propertySelector));
        }
    }
    
    public class SmartActionController<TEntity>
    {
        private readonly SmartController<TEntity> _smartController;
        private readonly Expression<Action<TEntity>> _actionSelector;

        public SmartActionController(SmartController<TEntity> smartController, Expression<Action<TEntity>> actionSelector)
        {
            _smartController = smartController;
            _actionSelector = actionSelector;
        }

        public void AddBehavior(ISmartActionBehavior actionBehavior)
        {
            _smartController.AddHandler(new SmartActionHandler<TEntity>(_actionSelector, actionBehavior));
        }
    }

    public class SmartActionHandler<TEntity> : ISmartHandler
    {
        private readonly Expression<Action<TEntity>> _actionSelector;
        private readonly ISmartActionBehavior _actionBehavior;

        public SmartActionHandler(Expression<Action<TEntity>> actionSelector, ISmartActionBehavior actionBehavior)
        {
            _actionSelector = actionSelector;
            _actionBehavior = actionBehavior;
        }

        public void Handle(IInvocation invocation, SmartContext smartContext, object entity)
        {
            var method = ((MethodCallExpression) _actionSelector.Body).Method;
            if (invocation.Method.IsImplementationOf(method))
            {
                _actionBehavior.BeforeInvocation(invocation.Method);
            }
        }
    }
}