using ConsoleApp.Lib;
using DuckType;
using DuckType.Core.Smart;

namespace ConsoleApp.HelloWorld.Native
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creating  a custom resolver since the SmartVampire Attribute on Foo is dependent 
            // on the IDayNightProvider and needs to be able to resolve the DayNightProvider.
            //
            // This is done to prove that DI can be used inside an attribute.
            //
            // However, in real live scenario's it is recommended to either
            //    - Use a proven external DI container.
            //      <OR>
            //    - Do not create custom attributes depending on IoC.  
            SmartObjectFactory.SetResolver(new MyResolver());
            
            var app = new Application(new Foo().MakeSmart());
            
            app.Run();
        }
    }
}