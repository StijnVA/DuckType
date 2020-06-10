using System;
using System.Reflection;

namespace SovitexLib.Core.Smart.Behaviors
{
    public class Vampire : ISmartActionBehavior
    {
        private readonly IDayNightProvider _dayNightProvider;

        public Vampire(IDayNightProvider dayNightProvider)
        {
            _dayNightProvider = dayNightProvider;
        }
        public void BeforeInvocation(MethodInfo invocationMethod)
        {
            if(_dayNightProvider.IsDayLight())
                throw new Exception($"Vampires can not {invocationMethod.Name} during daylight");
        }
    }

    public interface IDayNightProvider
    {
        bool IsDayLight();
    }
}