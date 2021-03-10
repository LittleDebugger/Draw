# Draw
Drawing application engine and console implementation with simple functionality

Written in C# .NET targeting .NET Core 3.1

Minimum .NET Core 3.1 requirements
https://dotnet.microsoft.com/download/dotnet/3.1

## Nuget packages:
- Microsoft.Extensions.DependencyInjection
- Microsoft.NET.Test.Sdk
- Microsoft.Extensions.Logging.Abstractions
- Moq
  
## How to run:  
Run from Visual Studio 2019: 
startup project Draw.Console

Run from command line:
cd .\Source\Draw.Console
dotnet run
  
## Assumptions:
- Canvas width or height will never be greater than int.MaxValue
- Command part of input is case insensitive

(Test suite is not exhaustive)

  
## Design choices:  
The core functionality of Draw is in the Draw.Core project. This project encapsulates functions and abstract data types
which are not tied to any specific technology for human interface (in this case, the keyboard and console window). The design is pluggable so that other implementations can be added later to support, mouse, graphics, etc. (the core  functionality is also very extensible.)  This segregation means that implementations can be developed by different teams (or companies if the Core were packaged up). 
Each new implementation would benefit from the existing core code meaning that the existing functionality would not have to be rewritten, just the required adapters and implementations would need to be coded.

The fundamental building block of the Core is the Pixel. - A single unit of the canvas. In the case it is a character but in 
others it could be a data type with many different properties (colour, hue, transparency).
Without wanting to make too many assumptions about this, the pixel data type has been made generic so that implementations 
with different properties and behaviour can be used for different implementations of the Core. This does make interaction 
with the pixel a little cumbersome which could be reviewed later if a complete definition of a pixel were agreed upon.

The event loop (DrawEngine) is currently very simple. It handles this keyboard/console implementation fine but it would 
probably need to be redesigned as the core was consumed in different implementations.

