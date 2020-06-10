using Castle.DynamicProxy;

namespace SovitexLib.Core.Smart
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
                typeof(ISmartObject<TEntity>).GetProperty(nameof(ISmartObject<TEntity>.SmartController)).GetMethod)
            {
                invocation.ReturnValue = _smartController;
            }
            else
            {
                var smartContext = new SmartContext();
                _smartController.HandleBefore(invocation, smartContext);
                invocation.Proceed();
            }
        }
    }
}