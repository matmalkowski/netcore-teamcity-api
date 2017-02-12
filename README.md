[![Build status](https://ci.appveyor.com/api/projects/status/wms9wdqb4a109c1h?svg=true)](https://ci.appveyor.com/project/monkey3310/netcore-teamcity-api)
# NetCoreTeamCity - TeamCity client library for .NET Core
NetCoreTeamCity is a client library targeting .netcore (.NET Platform 1.6) that provides an easy
way to interact with the [TeamCity API](https://confluence.jetbrains.com/display/TCD10/REST+API). 

# Getting started
When 0.1.0 version is done, that supports build related endpoint queries and requests, it will be avaiable as a pre-release nuget package

# Using NetCoreTeamCity
Basic usage scenario would be getting detailed info of a build with known id:
```csharp
var teamCity = new TeamCity("host", "username", "password");
var build = teamCity.Builds.Get(123);
```
Above code would return build located by build id 123

It is possible to find builds with search criteria, that are passed into query with fluent builder class, for example, to return builds from project named "projectName" that were run on "customBranch" and were added to the build queue no longer then a day from now:
```csharp
var builds = teamCity.Builds.Find(
                By.Build
                    .QueuedDateAfter(DateTime.Now.AddDays(-1))
                    .Project("projectName")
                    .Branch("customBranch"));
```
# Project state

Right know only basic build related call are beeing supported as I progress with the dev, more endpoints will be added. As it is my off-work project it will take some time until client will cover all avaiable API requests.
Also, right know I'm only targeting .NET Core as its using .NET Platform v1.6. Upon introduction of .NET Platform 2.0 this library will support both .NET Core and .NET 4.6.1 applications.
