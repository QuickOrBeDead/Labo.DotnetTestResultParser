dotnet-labodotnettestresultsparser
============

[![License](http://img.shields.io/:license-mit-blue.svg)](https://quickorbedead.mit-license.org)
[![NuGet][main-nuget-badge]][main-nuget] 
[![NuGet][nuget-dl-badge]][main-nuget]

[main-nuget]: https://www.nuget.org/packages/dotnet-labodotnettestresultsparser/
[main-nuget-badge]: https://img.shields.io/nuget/v/dotnet-labodotnettestresultsparser.svg?style=flat-square&label=nuget
[nuget-dl-badge]: https://img.shields.io/nuget/dt/dotnet-labodotnettestresultsparser.svg?style=flat-square
[![Travis Build Status](https://img.shields.io/travis/QuickOrBeDead/Labo.DotnetTestResultParser.svg)](https://travis-ci.org/QuickOrBeDead/Labo.DotnetTestResultParser)
[![Maintainability](https://api.codeclimate.com/v1/badges/c9c344a99a98f8862166/maintainability)](https://codeclimate.com/github/QuickOrBeDead/Labo.DotnetTestResultParser/maintainability)
[![Coverage Status](https://coveralls.io/repos/github/QuickOrBeDead/Labo.DotnetTestResultParser/badge.svg?branch=master)](https://coveralls.io/github/QuickOrBeDead/Labo.DotnetTestResultParser?branch=master)
[![CII Best Practices](https://bestpractices.coreinfrastructure.org/projects/2144/badge)](https://bestpractices.coreinfrastructure.org/projects/2144)

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
  --version                     Show version information
  -?|-h|--help                  Show help information
  -f|--format                   Unit test result xml format. (Default: NUnit)
  -o|--output                   Output file to write results. (Default output is Console)
  -t|--template                 The output template. Allowed values are: Summary, TestResult. (Default: Summary)
  --fail-when-result-is-failed  Fails the program when the unit test result is 'Failed'.
```

## Examples

### Parse Test Reults Xml and Fail Program if Test Result is Failed

```
$ dotnet labodotnettestresultsparser <path> -f NUnit --fail-when-result-is-failed
```

### Writes Only the Test Result Text (Passed | Failed) to the Output Text

```
$ dotnet labodotnettestresultsparser <path> -f NUnit -o <output.txt> -t TestResult
```

#### Output:

```
$ Passed
```

### Writes Only the Test Result  Text (Passed | Failed) for Multiple Unit Test Result Xmls to the Output Text

```
$ dotnet labodotnettestresultsparser "/testresults/*.unittest.xml" -f NUnit -o <output.txt> -t TestResult
```

#### Output:

```
$ Passed
```

### Bash script that checks Unit Test Results for multiple xmls

```bash
dotnet labodotnettestresultsparser "/testresults/*.unittest.xml" -f NUnit -o /testresults/result.txt -t TestResult

if [ ! -f /testresults/result.txt ]; then
    echo "Test Result File not found!"
    exit 1
fi

result=$(head -n 1 /testresults/result.txt)

if [ "$result" == "Failed" ]
then
    echo "Unit Tests Failed!"
    exit 1
else
   echo "Unit Tests Passed!"
fi
```

#### Output:

```
$ Unit Tests Passed!
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

Output is located at ```src/Labo.DotnetTestResultParser/nupkg```

### Uninstall

```
dotnet tool uninstall -g dotnet-labodotnettestresultsparser
```

## Useful Links

* [.NET Core 2.1 Global Tools Annoucement](https://blogs.msdn.microsoft.com/dotnet/2018/02/27/announcing-net-core-2-1-preview-1/#global-tools)
* [.NET Core Global Tools Sample](https://github.com/dotnet/core/blob/master/samples/dotnetsay/README.md)
* [.NET Core Global Tools and Gotchas](https://www.natemcmaster.com/blog/2018/02/02/dotnet-global-tool/)
* [NUnit Test Result XML Format](https://github.com/nunit/docs/wiki/Test-Result-XML-Format)
