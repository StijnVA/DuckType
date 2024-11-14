using System;
using DuckType.Core.Smart.Behaviors;

namespace ConsoleApp.Lib
{
    /// <summary>
    /// 
    /// </summary>
    public class DayNightProvider : IDayNightProvider
    {
        public bool IsDayLight()
        {
            //naive implementation
            return 8 < DateTime.Now.Hour  && DateTime.Now.Hour < 22;
        }
    }
}