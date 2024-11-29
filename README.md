# DuckType Library

Consider this still a Beta version.

## Overview

The **DuckType Library** is designed to enable "duck typing" in .NET, allowing objects to be dynamically cast to interfaces with compatible methods and properties. It also introduces a "smart" object mechanism, which enhances objects with additional behaviors, validation, and default implementations.

This library is primarily intended for scenarios where interfaces must be dynamically applied to objects that do not explicitly implement them, enabling more flexible coding practices without needing concrete implementations. It also supports validation and behavior customization through "smart" objects, making it suitable for more advanced scenarios requiring enhanced property management.

---

## Getting Started

### Installation

Add the library to your project.

```shell
dotnet add package DuckType
```

### Basic Usage
See https://github.com/StijnVA/DuckType/wiki for the getting started

## Acknowledgement

The heavy lifting was already been done by [Castle.Core](https://www.castleproject.org/).

## Contributing

We welcome contributions! Please refer to `CONTRIBUTING.md` for details on how to participate.

---

## License

This library is released under the MIT License. See `LICENSE` for details.

