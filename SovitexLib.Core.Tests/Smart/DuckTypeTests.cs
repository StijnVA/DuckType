using FluentAssertions;
using SovitexLib.Core.DuckType;
using SovitexLib.Core.Smart;
using SovitexLib.Core.Tests.Dummies;
using Xunit;

namespace SovitexLib.Core.Tests.Smart
{
    public class DuckTypeTests
    {
        [Fact]
        public void DuckTypeAnAnonymousObject()
        {
            var anonymous = new {MyProperty = "Test"};
            var foo = anonymous.DuckType<IFoo>();
            foo.Should().BeAssignableTo<IFoo>();
            foo.MyProperty.Should().Be("Test");
        }
        
        [Fact]
        public void DuckTypeWriteProperty()
        {
            var foo = new Foo {MyProperty = "Test"};
            var duck = foo.DuckType<IFoo>();
            duck.MyProperty = "New Value";
            duck.MyProperty.Should().Be("New Value");
        }
        
        [Fact]
        public void DuckTypeBarAsFoo()
        {
            var bar = new Bar {MyProperty = "Test"};
            var duck = bar.DuckType<IFoo>();
            duck.MyProperty = "New Value";
            duck.MyProperty.Should().Be("New Value");
        }
        
        [Fact]
        public void DuckTypeBazAsFoo()
        {
            var bar = new Baz();
            var duck = bar.DuckType<IFoo>();
            duck.Invoking(d => d.MyProperty).Should().Throw<DuckTypeException>();
        }
        
        [Fact]
        public void DoStuffOnAnonymous()
        {
            var bar = new {};
            var duck = bar.DuckType<IQuux>();
            duck.Invoking(d => d.DoStuff()).Should().Throw<DuckTypeException>();
        }
        
        [Fact]
        public void DoStuffOnQuux()
        {
            var quux = new Quux();
            var duck = quux.DuckType<IQuux>();
            duck.Invoking(d => d.DoStuff()).Should().NotThrow();
            quux.StuffHasBeenDone.Should().BeTrue();
            quux.DoOtherStuff().Should().BeTrue();
        }
        
        [Fact]
        public void DoStuffOnAnonymousWithDefaultImplementation()
        {
            var bar = new {};
            var duck = bar.DuckType<IQuux>(options => options.UseDefaultImplementations());
            
            duck.Invoking(d => d.DoStuff()).Should().NotThrow();
            duck.Invoking(d => d.DoOtherStuff()).Should().NotThrow().Subject.Should().BeFalse();
        }
    }
}