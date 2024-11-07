using DuckType.Core.Smart.Attributes;

namespace DuckType.Core.Tests.Dummies
{
    public class MyClass
    {
        [SmartEmail] 
        public virtual string EmailAddress { get; set; }
    }
}