using System;

namespace ConsoleApp.HelloWorld
{
    internal class Application
    {
        private readonly IFoo _foo;

        public Application(IFoo foo)
        {
            _foo = foo;
        }
        
        public void Run()
        {
            try
            {
                _foo.DoStuff();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                _foo.Max = 10;
                _foo.Value = 5;
                _foo.Value = 15; //should fail
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}