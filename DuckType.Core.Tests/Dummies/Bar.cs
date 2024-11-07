namespace DuckType.Core.Tests.Dummies
{
    //Bar does look like IFoo, but does not implements it
    public class Bar
    {
        public string MyProperty { get; set; }
    }
    
    //Baz does not look like IFoo
}