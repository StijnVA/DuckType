using System;
using DuckType.Adaptors.FluentValidation;
using DuckType.Core.Smart.Attributes;

namespace ConsoleApp.HelloWorld
{
    [SmartFluentValidation(typeof(FooValidator))]
    public class Foo : IFoo
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public int Value { get; set; }
        
        [SmartVampire]
        public void DoStuff()
        {
            Console.WriteLine("I am doing stuff");
        }
    }
}