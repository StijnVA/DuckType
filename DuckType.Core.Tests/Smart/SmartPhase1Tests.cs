using System;
using DuckType.Core.Smart;
using DuckType.Core.Smart.Behaviors;
using DuckType.Core.Tests.Dummies;
using FluentAssertions;
using Moq;
using Xunit;

namespace DuckType.Core.Tests.Smart
{
    public class SmartPhase1Tests
    {
        [Fact]
        public void TheSmartEmailPropertyCanBeSetWithAValideValue_Virtual()
        {
            var original = new MyClass();
            var smart = original.MakeSmart();
            smart.EmailAddress = "eddy.freddy@example.com";
            smart.EmailAddress.Should().Be("eddy.freddy@example.com");
        }

        [Fact]
        public void TheSmartEmailPropertyCanBeSetWithAValideValue_Interface()
        {
            var original = (IMyClass) new MyClassFromInterface();
            var smart = original.MakeSmart();
            smart.EmailAddress = "eddy.freddy@example.com";
            smart.EmailAddress.Should().Be("eddy.freddy@example.com");
        }

        [Fact]
        public void TheSmartEmailPropertyShouldThrowAnExceptionWhenSetWithAnInvalidValue_Fluent()
        {
            var original = new MyClass();
            var smart = original.MakeSmart();
            smart.GetSmartController()
                .ForProperty(e => e.EmailAddress)
                .AddBehavior(new AllowOnlyEmailAddress());
            smart.Invoking(s => s.EmailAddress = "notAnEmailAddress").Should().Throw<SmartException>();
        }

        [Fact]
        public void TheSmartEmailPropertyShouldThrowAnExceptionWhenSetWithAnInvalidValue_Attribute()
        {
            var original = new MyClassWithAttribute();
            var smart = original.MakeSmart();
            smart.Invoking(s => s.EmailAddress = "notAnEmailAddress").Should().Throw<SmartException>();
        }

        [Fact]
        public void TheSmartEmailPropertyShouldThrowAnExceptionWhenSetWithAnInvalidValue_AttributeInterface()
        {
            var original = (IMyClass) new MyClassFromInterface();
            var smart = original.MakeSmart();
            smart.Invoking(s => s.EmailAddress = "notAnEmailAddress").Should().Throw<SmartException>();
        }

        [Fact]
        public void VampireDoNothingDuringDayLight()
        {
            var dayNightProvider = Mock.Of<IDayNightProvider>();
            Mock.Get(dayNightProvider)
                .Setup(e => e.IsDayLight())
                .Returns(true);


            var original = (IQuux) new Quux();
            var smart = original.MakeSmart();
            smart.GetSmartController()
                .ForAction(e => e.DoStuff())
                .AddBehavior(new VampireBehavior(dayNightProvider));
            smart.Invoking(s => s.DoStuff()).Should().Throw<Exception>();
        }

        [Fact]
        public void VampireDoStuffDuringNight()
        {
            var dayNightProvider = Mock.Of<IDayNightProvider>();
            Mock.Get(dayNightProvider)
                .Setup(e => e.IsDayLight())
                .Returns(false);

            var original = (IQuux) new Quux();
            var smart = original.MakeSmart();
            smart.GetSmartController()
                .ForAction(e => e.DoStuff())
                .AddBehavior(new VampireBehavior(dayNightProvider));
            smart.Invoking(s => s.DoStuff()).Should().NotThrow<Exception>();
        }
    }

    // ReSharper disable once MemberCanBePrivate.Global
}