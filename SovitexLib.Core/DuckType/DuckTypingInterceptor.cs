using System.Linq;
using Castle.DynamicProxy;
using SovitexLib.Core.Extensions;

namespace SovitexLib.Core.DuckType
{
    public class DuckTypingInterceptor<T> : IInterceptor
    {
        private readonly object _original;
        private readonly IDuckTypeOptionsReader _options;

        private const bool InvocationHasBeenHandled = true;
        private const bool InvocationHasNotBeenHandled = false;

        public DuckTypingInterceptor(object original, IDuckTypeOptionsReader options)
        {
            _original = original;
            _options = options;
        }

        public void Intercept(IInvocation invocation)
        {
            if (!(
                HandleReadProperty(invocation) ||
                HandleWriteProperty(invocation) ||
                HandleOtherMethods(invocation) ||
                HandleDefaultImplementation(invocation)))
            {
               throw new DuckTypeException($"Unable to handle {invocation.Method.Name}.");
            }
        }

        private bool HandleDefaultImplementation(IInvocation invocation)
        {
            if (!_options.UseDefaultImplementation) return InvocationHasNotBeenHandled;

            if (invocation.Method.ReturnType != typeof(void))
            {
                invocation.ReturnValue = invocation.Method.ReturnType.GetDefault();
            }

            return InvocationHasBeenHandled;
        }

        private bool HandleOtherMethods(IInvocation invocation)
        {
            var originalMethod = _original.GetType().GetMethod(invocation.Method.Name);

            if (originalMethod == null)
            {
                if (_options.UseDefaultImplementation) return InvocationHasNotBeenHandled;
                throw new DuckTypeException($"The method '{invocation.Method.Name}' is not defined");
            }

            originalMethod.Invoke(_original, invocation.Arguments);
            return InvocationHasBeenHandled;
        }

        private bool HandleReadProperty(IInvocation invocation)
        {
            var property = typeof(T).GetProperties().SingleOrDefault(p => p.GetMethod == invocation.Method);
            if (property == null) return InvocationHasNotBeenHandled;

            var originalProperty = _original.GetType().GetProperty(property.Name)
                                   ?? throw new DuckTypeException($"The property '{property.Name}' is not defined");
            
            var value = originalProperty.GetValue(_original, null);
            invocation.ReturnValue = value;
            return InvocationHasBeenHandled;
        }
        
        private bool HandleWriteProperty(IInvocation invocation)
        {
            var property = typeof(T).GetProperties().SingleOrDefault(p => p.SetMethod == invocation.Method);
            if (property == null) return InvocationHasNotBeenHandled;
            var originalProperty = _original.GetType().GetProperty(property.Name)
                                   ?? throw new DuckTypeException($"The property '{property.Name}' is not defined");
            
            var value = invocation.Arguments.Single();
            originalProperty.SetValue(_original, value);
            return InvocationHasBeenHandled;
        }
    }
}