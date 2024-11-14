using DuckType.Attributes;

namespace DuckType.Core.Tests.Dummies
{
    public class MyClassWithAttribute
    {
        [SmartEmail] 
        public virtual string EmailAddress { get; set; }
    }
}