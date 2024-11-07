using System;

namespace DuckType.Core.DuckType
{
    public class DuckTypeException : Exception
    {
        public DuckTypeException(string message): base(message)
        {
        }
    }
}