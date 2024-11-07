namespace DuckType.Core.Tests.Dummies
{
    public class Quux : IQuux
    {
        public bool StuffHasBeenDone { get; private set; }
        
        public void DoStuff()
        {
            StuffHasBeenDone = true;
        }

        public bool DoOtherStuff()
        {
            return true;
        }
    }
}