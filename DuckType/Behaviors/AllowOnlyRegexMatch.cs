using System.Text.RegularExpressions;
using DuckType.Core.Smart;

namespace DuckType.Behaviors
{
    public abstract class AllowOnlyRegexMatch : ISmartPropertyBehavior<string>
    {
        private readonly string _regex;

        protected AllowOnlyRegexMatch(string regex)
        {
            _regex = regex;
        }
        public void BeforeSetValue(string value, SmartContext _)
        {
            if (!Regex.IsMatch(value, _regex))
            {
                throw new SmartException($"The value '{value}' is not a valid email address");
            }
        }
    }
}