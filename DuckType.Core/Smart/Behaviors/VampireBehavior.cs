using System;
using System.Reflection;

namespace DuckType.Core.Smart.Behaviors
{
    public class VampireBehavior : ISmartActionBehavior
    {
        private readonly IDayNightProvider _dayNightProvider;

        public VampireBehavior(IDayNightProvider dayNightProvider)
        {
            _dayNightProvider = dayNightProvider;
        }
        public void BeforeInvocation(MethodInfo invocationMethod)
        {
            if(_dayNightProvider.IsDayLight())
                throw new Exception($"Vampires can't {invocationMethod.Name} during daylight");
        }
    }
}