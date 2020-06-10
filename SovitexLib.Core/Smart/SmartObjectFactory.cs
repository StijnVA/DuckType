using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Castle.DynamicProxy;

namespace SovitexLib.Core.Smart
{
    public class SmartObjectFactory
    {
        private static readonly SmartObjectFactory Instance = new SmartObjectFactory();
        
        public static T GenerateSmartObject<T>(T original) where T : class
        {
           return  Instance.GenerateSmartObjectInternal(original);
        }

        private readonly ProxyGenerator _proxyGenerator = new ProxyGenerator();

        private TEntity GenerateSmartObjectInternal<TEntity>(TEntity obj)
        {
            var smartController = new SmartController<TEntity>(obj);
            var interceptor = new SmartInterceptor<TEntity>(smartController);
            var additionalInterfacesToProxy = GetAdditionalInterfacesToProxy(obj).ToArray();
            var proxy = typeof(TEntity).IsInterface
                ? (TEntity) _proxyGenerator.CreateInterfaceProxyWithTarget(typeof(TEntity),
                    additionalInterfacesToProxy, obj, interceptor)
                : (TEntity) _proxyGenerator.CreateClassProxyWithTarget(typeof(TEntity),
                    additionalInterfacesToProxy, obj, interceptor);

            
            ConfigureSmartControllerFromAttributes(smartController);
            
            return proxy;
        }

        private void ConfigureSmartControllerFromAttributes<TEntity>(SmartController<TEntity> smartController)
        {
            var propertiesAndAttributes=smartController.Entity.GetType().GetProperties().Select(p => (p, p.GetCustomAttributes(true)));
            foreach (var (propertyInfo, attributes) in propertiesAndAttributes)
            {
                foreach (dynamic attribute in attributes)
                {
                    var entityType = smartController.Entity.GetType();
                    var parameter = Expression.Parameter(entityType, "entity");
                    var property = Expression.Property(parameter, propertyInfo);
                    var funcType = typeof(Func<,>).MakeGenericType(entityType, propertyInfo.PropertyType);
                    var propertySelector = Expression.Lambda(funcType, property, parameter);
                    
                    var type = typeof(SmartPropertyHandler<,>)
                        .MakeGenericType(
                            entityType,
                            propertyInfo.PropertyType);
                    var handler = Activator.CreateInstance(type, attribute.GetBehavior(), propertySelector);
                    smartController.AddHandler(handler);
                }
            }
        }

        private static IEnumerable<Type> GetAdditionalInterfacesToProxy<TEntity>(TEntity obj)
        {            
            //The proxy should be recognisable as an instance of all base types of the object
            var type = obj.GetType();
            while (type != null)
            {                                
                yield return typeof(ISmartObject<>).MakeGenericType(type);
                type = type.BaseType;
            }

            //The proxy should implement all interfaces that the object implements
            foreach (var @interface in obj.GetType().GetInterfaces())
            {
                yield return @interface;
                yield return typeof(ISmartObject<>).MakeGenericType(@interface);
            }
        }
        
    
    }
}