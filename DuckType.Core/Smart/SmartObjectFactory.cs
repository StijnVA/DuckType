using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Castle.DynamicProxy;
using DuckType.Internals;

namespace DuckType.Core.Smart
{
    public class SmartObjectFactory
    {
        private static readonly SmartObjectFactory Instance = new SmartObjectFactory();

        public static void SetResolver(IResolver resolver)
        {
            Instance.SetResolverInternal(resolver);
        }

        private void SetResolverInternal(IResolver resolver)
        {
            _resolver = resolver;
        }

        public static T GenerateSmartObject<T>(T original) where T : class
        {
           return  Instance.GenerateSmartObjectInternal(original);
        }

        private readonly ProxyGenerator _proxyGenerator = new ProxyGenerator();
        private IResolver _resolver;

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
            
            ConfigureSmartControllerFromPropertyAttributes(smartController);
            ConfigureSmartControllerFromActionAttributes(smartController);
            ConfigureSmartControllerFromClassAttributes(smartController);
            
            return proxy;
        }

        private void ConfigureSmartControllerFromClassAttributes<TEntity>(SmartController<TEntity> smartController)
        {
            var attributes = smartController.Entity.GetType().GetCustomAttributes(typeof(ISmartAttribute), true).Cast<ISmartAttribute>();
            foreach (var attribute in attributes)
            {
                smartController.AddHandler(new SmartClassHandler((ISmartClassBehavior)attribute.GetBehavior(_resolver)));
            }
        }

        private void ConfigureSmartControllerFromPropertyAttributes<TEntity>(SmartController<TEntity> smartController)
        {
            var propertiesAndAttributes=smartController.Entity.GetType().GetProperties().Select(p => (p, p.GetCustomAttributes(typeof(ISmartAttribute),true).Cast<ISmartAttribute>()));
            foreach (var (propertyInfo, attributes) in propertiesAndAttributes)
            {
                foreach (var attribute in attributes)
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
                    dynamic handler = Activator.CreateInstance(type, attribute.GetBehavior(_resolver), propertySelector);
                    smartController.AddHandler(handler);
                }
            }
        }

        private void ConfigureSmartControllerFromActionAttributes<TEntity>(SmartController<TEntity> smartController)
        {
            var actionsAndAttributes=smartController.Entity.GetType().GetMethods().Select(p => (p, p.GetCustomAttributes(typeof(ISmartAttribute),true).Cast<ISmartAttribute>()));
            foreach (var (memberInfo, attributes) in actionsAndAttributes)
            {
                foreach (var attribute in attributes)
                {
                    var behavior =  attribute.GetBehavior(_resolver) as ISmartActionBehavior; 
                    smartController.AddHandler(new SmartActionHandler<TEntity>(memberInfo, behavior));
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