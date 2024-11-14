using DuckType.Core.DuckType;
using DuckType.Core.Tests.Dummies;
using FluentAssertions;
using DuckType;
using Xunit;

namespace DuckType.Core.Tests.Smart
{
    public class DuckTypeTests
    {
        [Fact]
        public void DuckTypeAnAnonymousObject()
        {
            var anonymous = new {MyProperty = "Test"};
            var foo = anonymous.AsDuck<IFoo>();
            foo.Should().BeAssignableTo<IFoo>();
            foo.MyProperty.Should().Be("Test");
        }
        
        [Fact]
        public void DuckTypeWriteProperty()
        {
            var foo = new Foo {MyProperty = "Test"};
            var duck = foo.AsDuck<IFoo>();
            duck.MyProperty = "New Value";
            duck.MyProperty.Should().Be("New Value");
        }
        
        [Fact]
        public void DuckTypeBarAsFoo()
        {
            var bar = new Bar {MyProperty = "Test"};
            var duck = bar.AsDuck<IFoo>();
            duck.MyProperty = "New Value";
            duck.MyProperty.Should().Be("New Value");
        }
        
        [Fact]
        public void DuckTypeBazAsFoo()
        {
            var bar = new Baz();
            var duck = bar.AsDuck<IFoo>();
            duck.Invoking(d => d.MyProperty).Should().Throw<DuckTypeException>();
        }
        
        [Fact]
        public void DoStuffOnAnonymous()
        {
            var bar = new {};
            var duck = bar.AsDuck<IQuux>();
            duck.Invoking(d => d.DoStuff()).Should().Throw<DuckTypeException>();
        }
        
        [Fact]
        public void DoStuffOnQuux()
        {
            var quux = new Quux();
            var duck = quux.AsDuck<IQuux>();
            duck.Invoking(d => d.DoStuff()).Should().NotThrow();
            quux.StuffHasBeenDone.Should().BeTrue();
            quux.DoOtherStuff().Should().BeTrue();
        }
        
        [Fact]
        public void DoStuffOnAnonymousWithDefaultImplementation()
        {
            var bar = new {};
            var duck = bar.AsDuck<IQuux>(options => options.UseDefaultImplementations());
            
            duck.Invoking(d => d.DoStuff()).Should().NotThrow();
            duck.Invoking(d => d.DoOtherStuff()).Should().NotThrow().Subject.Should().BeFalse();
        }
    }
}