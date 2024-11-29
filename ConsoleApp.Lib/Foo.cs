using System;
using DuckType.Adaptors.FluentValidation;
using DuckType.Extensions.Attributes;

namespace ConsoleApp.Lib
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