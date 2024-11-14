using System;
using System.Reflection;
using DuckType.Core.Smart;
using DuckType.Core.Smart.Behaviors;

namespace DuckType.Extensions.Behaviors
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