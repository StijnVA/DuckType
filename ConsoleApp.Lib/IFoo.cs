namespace ConsoleApp.Lib
{
    public interface IFoo
    {
        int Min { get; set; }
        int Max { get; set; }
        int Value { get; set; }
        void DoStuff();
    }
}