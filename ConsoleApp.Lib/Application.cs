using System;

namespace ConsoleApp.Lib
{
    public class Application
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
                _foo.Value = 15; //will throw exception
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}