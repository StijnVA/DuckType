# DuckType and SmartObject Library

## Overview

The **DuckType and SmartObject Library** is designed to enable "duck typing" in .NET, allowing objects to be dynamically cast to interfaces with compatible methods and properties. It also introduces a "smart" object mechanism, which enhances objects with additional behaviors, validation, and default implementations.

This library is primarily intended for scenarios where interfaces must be dynamically applied to objects that do not explicitly implement them, enabling more flexible coding practices without needing concrete implementations. It also supports validation and behavior customization through "smart" objects, making it suitable for more advanced scenarios requiring enhanced property management.

---

## Key Features

1. **Duck Typing**: Allows casting objects to interfaces based on compatible signatures, even if the objects do not explicitly implement the interface.
2. **Smart Objects**: Enhances objects by adding validation, default behaviors, and dynamic behavior customization.
3. **Fluent Behavior Configuration**: Provides a fluent API to configure and apply behaviors on properties or methods.
4. **Default Implementations**: Enables interfaces with default implementations, which can be used when the object lacks specific method implementations.
5. **Behavioral Modifications**: Allows adding custom behaviors such as email validation or conditional method execution based on external conditions (e.g., day or night).

---

## Getting Started

### Installation

Add the library to your project.

```shell
dotnet add package DuckType.Core
```

### Basic Usage

1. **Duck Typing Objects**

   The `AsDuck<T>()` method allows you to cast any object to a specified interface `T` if the object’s properties and methods match the interface’s requirements.

   ```csharp
   var anonymous = new { MyProperty = "Hello" };
   var duck = anonymous.AsDuck<IFoo>();
   DoStuffWithAnIDuck(duck);  // Would not be possible with anonymouse
   ```

2. **Creating Smart Objects**

   Smart objects are created using the `MakeSmart()` method, enabling additional behaviors and validation on properties.

   ```csharp
        [SmartEmail] 
        public string EmailAddress { get; set; }
   ```
   ```csharp
        var smart = original.MakeSmart();
        
        //Will throw a validation exception
        smart.EmailAddress = "not an email adress"
   ```
   Given the custom attribute:

   ```csharp
        public class AllowOnlyEmailAddress : AllowOnlyRegexMatch
        {
            //Credits goes to bortzmeyer https://stackoverflow.com/a/201378/2968001
            const string ValidEmailRegex = @"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])";

            public AllowOnlyEmailAddress() : base(ValidEmailRegex)  { }
        }
   ```


---

## Duck Typing Examples

The **DuckTypeTests** suite demonstrates how to apply duck typing to various objects.

- **DuckTypeAnAnonymousObject**: Shows casting an anonymous object to an interface.
- **DuckTypeWriteProperty**: Verifies property modification on a duck-typed object.
- **DuckTypeBazAsFoo**: Demonstrates type mismatching that results in an exception.

### Example: Cast and Modify Properties

```csharp
var foo = new Foo { MyProperty = "Initial" };
var duck = foo.AsDuck<IFoo>();
duck.MyProperty = "Modified";
Console.WriteLine(duck.MyProperty);  // Outputs: Modified
```

---

## Smart Object Examples

The **SmartBasicTests** and **SmartPhase1Tests** demonstrate using smart objects with validation and custom behaviors.

- **SmartObjectShouldEqualsTheOriginal**: Ensures that smart objects retain original property values and structure.
- **TheSmartEmailPropertyCanBeSetWithAValidValue**: Shows how to validate email addresses using custom behaviors.
- **VampireDoNothingDuringDayLight**: Implements conditional method execution based on time of day, using `VampireBehavior`.

### Example: Applying Custom Behaviors

```csharp
var original = new MyClass();
var smart = original.MakeSmart();
smart.GetSmartController()
     .ForProperty(e => e.EmailAddress)
     .AddBehavior(new AllowOnlyEmailAddress());

smart.EmailAddress = "valid.email@example.com"; // Works fine
smart.EmailAddress = "invalid";                 // Throws SmartException
```

---

## Additional Functionalities

### Configuring Default Implementations

In cases where methods are not implemented on the object, the library can provide default implementations using the `UseDefaultImplementations` option.

```csharp
var anonymous = new {};
var duck = anonymous.AsDuck<IQuux>(options => options.UseDefaultImplementations());
duck.DoStuff();  // Executes the default implementation
```

### Using Behaviors with External Conditions

Behavior customization allows for complex interactions. For example, methods can be executed only under certain conditions, such as time-dependent behavior.

```csharp
var dayNightProvider = Mock.Of<IDayNightProvider>();
Mock.Get(dayNightProvider).Setup(e => e.IsDayLight()).Returns(false);

var quux = new Quux();
var smartQuux = quux.MakeSmart();
smartQuux.GetSmartController()
         .ForAction(e => e.DoStuff())
         .AddBehavior(new VampireBehavior(dayNightProvider));

smartQuux.DoStuff();  // Executes normally if it’s nighttime
```

---

## Acknowledgement

The heavy lifting was already been done by [Castle.Core](https://www.castleproject.org/).

## Contributing

We welcome contributions! Please refer to `CONTRIBUTING.md` for details on how to participate.

---

## License

This library is released under the MIT License. See `LICENSE` for details.

