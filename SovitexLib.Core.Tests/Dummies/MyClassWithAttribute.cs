using SovitexLib.Core.Smart;

namespace SovitexLib.Core.Tests.Dummies
{
    public class MyClassWithAttribute
    {
        [SmartEmail] 
        public virtual string EmailAddress { get; set; }
    }
}