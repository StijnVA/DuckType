using SovitexLib.Core.Smart;

namespace SovitexLib.Core.Tests.Dummies
{
    public class MyClass
    {
        [SmartEmail] 
        public virtual string EmailAddress { get; set; }
    }
}