using Autofac;
using DuckType.Adaptors.Autofac;
using DuckType.Core.Smart;
using DuckType.Core.Smart.Behaviors;

namespace ConsoleApp.HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder
                .RegisterType<Application>()
                .AsSelf();
            
            containerBuilder
                .Register(ctx => ((IFoo)new Foo()).MakeSmart())
                .As<IFoo>();

            containerBuilder
                .RegisterType<DayNightProvider>()
                .As<IDayNightProvider>();

            containerBuilder
                .RegisterType<FooValidator>()
                .AsSelf();
            
            var container = containerBuilder.Build();
            
            using var lifeTimeScope = container.BeginLifetimeScope();
            SmartObjectFactory.SetResolver(new AutofacResolver(lifeTimeScope));
            var application = lifeTimeScope.Resolve<Application>();
            application.Run();
        }
    }
}