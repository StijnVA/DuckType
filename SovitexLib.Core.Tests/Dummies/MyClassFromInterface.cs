using SovitexLib.Core.Smart;

namespace SovitexLib.Core.Tests.Dummies
{
    public class MyClassFromInterface : IMyClass
    {
        [SmartEmail] 
        public string EmailAddress { get; set; }
    }
}