using System;
using System.Linq.Expressions;

namespace SovitexLib.Core.Smart
{
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
}