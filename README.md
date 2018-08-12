dotnet-labodotnettestresultsparser
============

[![NuGet][main-nuget-badge]][main-nuget] [![NuGet][nuget-dl-badge]][main-nuget]

[main-nuget]: https://www.nuget.org/packages/dotnet-labodotnettestresultsparser/
[main-nuget-badge]: https://img.shields.io/nuget/v/dotnet-labodotnettestresultsparser.svg?style=flat-square&label=nuget
[nuget-dl-badge]: https://img.shields.io/nuget/dt/dotnet-labodotnettestresultsparser.svg?style=flat-square


.Net Core Test Result Parser Global Tool

## Installation

### .NET Core 2.1 & higher
```
dotnet tool install --global dotnet-labodotnettestresultsparser
```
## Usage

### Help

```
$ dotnet labodotnettestresultsparser --help

dotnet-labodotnettestresultsparser

Usage: dotnet labodotnettestresultsparser [arguments] [options]

Arguments:
  path  The test result xml path.

Options:
  --version             Show version information
  -?|-h|--help          Show help information
  -f|--format           Unit test result xml format. (Default: NUnit)
```

### Parse Test Reults Xml

```
$ dotnet labodotnettestresultsparser <path> -f NUnit

```

## Build

```
git clone https://github.com/QuickOrBeDead/Labo.DotnetTestResultParser.git
```
```
cd Labo.DotnetTestResultParser/src/Labo.DotnetTestResultParser
```
```
dotnet pack -c release -o nupkg
```

Output is located in ```src/Labo.DotnetTestResultParser/nupkg```

### Uninstall

```
dotnet tool uninstall -g dotnet-labodotnettestresultsparser
```

## Useful Links

* [.NET Core 2.1 Global Tools Annoucement](https://blogs.msdn.microsoft.com/dotnet/2018/02/27/announcing-net-core-2-1-preview-1/#global-tools)
* [.NET Core Global Tools Sample](https://github.com/dotnet/core/blob/master/samples/dotnetsay/README.md)
* [.NET Core Global Tools and Gotchas](https://www.natemcmaster.com/blog/2018/02/02/dotnet-global-tool/)
* [NUnit Test Result XML Format](https://github.com/nunit/docs/wiki/Test-Result-XML-Format)
