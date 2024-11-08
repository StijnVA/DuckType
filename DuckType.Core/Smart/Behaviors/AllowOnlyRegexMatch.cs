﻿using System.Text.RegularExpressions;

namespace DuckType.Core.Smart.Behaviors
{
    public abstract class AllowOnlyRegexMatch : ISmartPropertyBehavior<string>
    {
        private readonly string _regex;

        protected AllowOnlyRegexMatch(string regex)
        {
            _regex = regex;
        }
        public void BeforeSetValue(string value)
        {
            if (!Regex.IsMatch(value, _regex))
            {
                throw new SmartException($"The value '{value}' is not a valid email address");
            }
        }
    }
}