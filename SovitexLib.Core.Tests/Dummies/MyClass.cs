using SovitexLib.Core.Smart.Attributes;

namespace SovitexLib.Core.Tests.Dummies
{
    public class MyClass
    {
        [SmartEmail] 
        public virtual string EmailAddress { get; set; }
    }
}