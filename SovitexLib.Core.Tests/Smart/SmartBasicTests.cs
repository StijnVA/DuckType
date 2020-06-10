using FluentAssertions;
using SovitexLib.Core.Smart;
using SovitexLib.Core.Tests.Dummies;
using Xunit;

namespace SovitexLib.Core.Tests.Smart
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