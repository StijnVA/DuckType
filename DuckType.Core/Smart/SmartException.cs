using System;

namespace DuckType.Core.Smart
{
    public class SmartException : Exception
    {
        public SmartException(string msg) : base(msg)
        {
        }
    }
}