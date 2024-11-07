using System;
using System.Linq.Expressions;

namespace DuckType.Core.Smart
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
}