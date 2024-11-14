using FluentValidation;

namespace ConsoleApp.Lib
{
    public class FooValidator : AbstractValidator<IFoo>
    {
        public FooValidator()
        {
            RuleFor(f => f.Value)
                .Must((foo, value) => foo.Min <= value && value <= foo.Max)
                .WithMessage(actual =>  $" Value {actual.Value} must be between {actual.Min} and {actual.Max}");
        }
    }
}