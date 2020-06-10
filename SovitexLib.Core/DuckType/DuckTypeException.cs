using System;

namespace SovitexLib.Core.DuckType
{
    public class DuckTypeException : Exception
    {
        public DuckTypeException(string message): base(message)
        {
        }
    }
}