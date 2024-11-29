using System;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using DuckType.Core.Extensions;

namespace DuckType.Core.Smart
{
    public class SmartContext
    {
        private readonly IInvocation _invocation;

        public SmartContext(IInvocation invocation)
        {
            _invocation = invocation;
        }

        public bool ShouldIgnoreChange { get; private set; }
        public string PropertyName => _invocation.Method.Name[4..]; 

        public object GetCurrentPropertyValue()
        {
            
            var propertyInfo = _invocation.TargetType.GetProperty(PropertyName)
                ?? throw new Exception($"Property '{PropertyName}' does not exist on type '{_invocation.TargetType.FullName}'");
            var value = propertyInfo.GetValue(_invocation.InvocationTarget);
            return value;
        }

        public MethodInfo GetInvokedMethod()
        {
            return _invocation.Method;
        }

        public object GetSetValue()
        {
            return _invocation.Arguments.Single();
        }

        public void CancelInvocation()
        {
            ShouldIgnoreChange = true;
        }

        public void SetNewTargetValue<T>(T targetValue) where T : IComparable
        {
            _invocation.Arguments[0] = targetValue;
        }
    }
}