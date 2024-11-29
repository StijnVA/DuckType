using System;
using DuckType;
using DuckType.Attributes;
using DuckType.Behaviors;
using FluentAssertions;
using Xunit;

namespace TestProject1DuckType.Tests
{
    public class BetweenBoundariesAttributeTests
    {
        [Fact]
        public void SetAValueBetweenBoundries()
        {
            var myClass = new MyClass { MyInt = 0 }.MakeSmart();
            myClass.MyInt = 4;
            myClass.MyInt.Should().Be(4);
        }
        
        [Fact]
        public void SetAValueOutsideBoundries()
        {
            var myClass = new MyClass { MyInt = 0 }.MakeSmart();
            var action = myClass.Invoking(e => e.MyInt = 6);
            action.Should().Throw<ArgumentOutOfRangeException>();
        }
        
        [Fact]
        public void SetAValueOutsideBoundriesIgnoreChange()
        {
            var myClass = new MyClass { MyIntIgnoreChange = 4 }.MakeSmart();
            myClass.MyIntIgnoreChange = 6;
            myClass.MyIntIgnoreChange.Should().Be(4);
        }
        
        [Fact]
        public void SetAValueOutsideBoundriesApplyBoundaryMax()
        {
            var myClass = new MyClass { MyIntApplyBoundary = 4 }.MakeSmart();
            myClass.MyIntApplyBoundary = 6;
            myClass.MyIntApplyBoundary.Should().Be(5);
        }
        
        [Fact]
        public void SetAValueOutsideBoundriesApplyBoundaryMin()
        {
            var myClass = new MyClass { MyIntApplyBoundary = 4 }.MakeSmart();
            myClass.MyIntApplyBoundary = -10;
            myClass.MyIntApplyBoundary.Should().Be(-5);
        }
        
        [Fact]
        public void SetAValueOutsideBoundriesLongApplyBoundaryMin()
        {
            var myClass = new MyClass { MyLongApplyBoundary = 4 }.MakeSmart();
            myClass.MyLongApplyBoundary = -10;
            myClass.MyLongApplyBoundary.Should().Be(-5);
        }
    }

    public class MyClass
    {
        [BetweenBoundariesInt(-5 , 5)]
        public virtual int MyInt { get; set; }
        
        [BetweenBoundariesInt(-5 , 5, CompensationBehavior.IgnoreChange)]
        public virtual int MyIntIgnoreChange { get; set; }
        
        [BetweenBoundariesInt(-5 , 5, CompensationBehavior.ApplyBoundary)]
        public virtual int MyIntApplyBoundary { get; set; }

        [BetweenBoundariesLong(-5 , 5, CompensationBehavior.ApplyBoundary)]
        public virtual long  MyLongApplyBoundary { get; set; }
    }
}