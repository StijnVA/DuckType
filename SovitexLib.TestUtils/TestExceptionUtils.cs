using System;
using System.Linq;

namespace SovitexLib.TestUtils
{
    public static class TestExceptionUtils
    {
        public static bool ScanForException<TException>(Exception exception) where TException : Exception
        {
            if (exception == null) return false;
            if (exception is TException) return true;
            if (ScanForException<TException>(exception.InnerException)) return true;
            return exception is AggregateException aggregateException &&
                   aggregateException.InnerExceptions.Any(ScanForException<TException>);
        }
    }
}