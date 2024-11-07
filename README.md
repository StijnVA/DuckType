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

Add the library to your project (assuming it’s available via NuGet or source).

```shell
dotnet add package DuckType.Core
```

### Basic Usage

1. **Duck Typing Objects**

   The `AsDuck<T>()` method allows you to cast any object to a specified interface `T` if the object’s properties and methods match the interface’s requirements.

   ```csharp
   var anonymous = new { MyProperty = "Hello" };
   var duck = anonymous.AsDuck<IFoo>();
   Console.WriteLine(duck.MyProperty);  // Outputs: Hello
   ```

2. **Creating Smart Objects**

   Smart objects are created using the `MakeSmart()` method, enabling additional behaviors and validation on properties.

   ```csharp
   var foo = new Foo { MyProperty = "Some Text" };
   var smartFoo = foo.MakeSmart();
   Console.WriteLine(smartFoo.MyProperty);  // Outputs: Some Text
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

## Contributing

We welcome contributions! Please refer to `CONTRIBUTING.md` for details on how to participate.

---

## License

This library is released under the MIT License. See `LICENSE` for details.

