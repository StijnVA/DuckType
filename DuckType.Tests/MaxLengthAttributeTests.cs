using System;
using DuckType;
using DuckType.Attributes;
using DuckType.Behaviors;
using FluentAssertions;
using Xunit;

namespace TestProject1DuckType.Tests
{
    public class MaxLengthAttributeTests
    {
        [Fact]
        public void Default()
        {
            var myClass = new MyClass().MakeSmart();
            myClass.MyStringProperty = "Hello World";
            myClass.MyStringProperty.Should().Be("Hello World");
        }
        
        [Fact]
        public void ShouldThrowWhenTooLong()
        {
            var myClass = new MyClass().MakeSmart();
            myClass.Invoking(e => e.MyStringProperty = "Hello World 2")
                .Should().Throw<Exception>();
        }
        
        [Fact]
        public void Should()
        {
            var myClass = new MyClass().MakeSmart();
            myClass.MyStringPropertyApplyBoundary = "Hello World 2";
            myClass.MyStringPropertyApplyBoundary.Should().Be("Hello World", because: "It should be the substring of maxLength");
        }


        public class MyClass
        {
            [MaxLength(11)]
            public virtual string MyStringProperty { get; set; }
            
            [MaxLength(11, CompensationBehavior.ApplyBoundary)]
            public virtual string MyStringPropertyApplyBoundary { get; set; }
        }
    }
}