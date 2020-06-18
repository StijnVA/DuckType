using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Castle.DynamicProxy;

namespace SovitexLib.Core.Smart
{
    public class SmartController<TEntity>
    {
        public TEntity Entity { get; }
        private readonly List<ISmartHandler> _smartHandlers = new List<ISmartHandler>();

        public SmartController(TEntity entity)
        {
            Entity = entity;
        }

        public SmartPropertyController<TEntity, TProperty> ForProperty<TProperty>(Expression<Func<TEntity, TProperty>> propertySelector)
        {
            return new SmartPropertyController<TEntity, TProperty>(this, propertySelector);
        }
        
        public SmartActionController<TEntity> ForAction(Expression<Action<TEntity>> actionSelector)
        {
            return new SmartActionController<TEntity>(this, actionSelector);
        }

        public void HandleBefore(IInvocation invocation, SmartContext smartContext)
        {
            foreach (var smartHandler in _smartHandlers)
            {
                (smartHandler as ISmartBeforeHandler)?.HandleBefore(invocation, smartContext, Entity);
            }
        }
        
        public void HandleAfter(IInvocation invocation, SmartContext smartContext)
        {
            foreach (var smartHandler in _smartHandlers)
            {
                (smartHandler as ISmartAfterHandler)?.HandleAfter(invocation, smartContext, Entity);
            }
        }

        public void AddHandler(ISmartHandler smartHandler)
        {
            _smartHandlers.Add(smartHandler);
        }

    
    }
}