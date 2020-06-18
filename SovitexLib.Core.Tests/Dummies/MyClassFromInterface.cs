using SovitexLib.Core.Smart.Attributes;

namespace SovitexLib.Core.Tests.Dummies
{
    public class MyClassFromInterface : IMyClass
    {
        [SmartEmail] 
        public string EmailAddress { get; set; }
    }
}