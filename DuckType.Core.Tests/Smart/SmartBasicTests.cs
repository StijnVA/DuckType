using DuckType.Core.Smart;
using DuckType.Core.Tests.Dummies;
using FluentAssertions;
using Xunit;

namespace DuckType.Core.Tests.Smart
{
    public class SmartBasicTests
    {
        [Fact]
        public void SmartObjectShouldEqualsTheOriginal()
        {
            IFoo original = new Foo {MyProperty = "Some Text"};
            var smart = original.MakeSmart();
            smart.MyProperty.Should().Be("Some Text");
            smart.Should().BeEquivalentTo(original);
        }
        
        [Fact]
        public void SmartObjectShouldImplementsISmartObject()
        {
            var original = new Foo {MyProperty = "Some Text"};
            var smart = original.MakeSmart();
            smart.Should().BeAssignableTo<ISmartObject>();
        }
    }
}