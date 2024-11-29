using Castle.DynamicProxy;

namespace DuckType.Core.Smart
{
    internal class SmartInterceptor<TEntity> : IInterceptor
    {
        private readonly SmartController<TEntity> _smartController;


        public SmartInterceptor(SmartController<TEntity> smartController)
        {
            _smartController = smartController;
        }

        public void Intercept(IInvocation invocation)
        {
            if (invocation.Method ==
                typeof(ISmartObject<TEntity>).GetProperty(nameof(ISmartObject<TEntity>.SmartController))?.GetMethod)
            {
                invocation.ReturnValue = _smartController;
            }
            else
            {
                var smartContext = new SmartContext(invocation);
                _smartController.HandleBefore(smartContext);
                if(smartContext.ShouldIgnoreChange)
                    return;
                
                invocation.Proceed();
                _smartController.HandleAfter(smartContext);
            }
        }
    }
}