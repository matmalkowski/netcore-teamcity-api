[![Build status](https://ci.appveyor.com/api/projects/status/wms9wdqb4a109c1h?svg=true)](https://ci.appveyor.com/project/monkey3310/netcore-teamcity-api)
[![NuGet Pre Release](https://img.shields.io/nuget/vpre/NetCoreTeamCity.svg)](https://www.nuget.org/packages/NetCoreTeamCity/)
# NetCoreTeamCity - TeamCity client library for .NET Core
NetCoreTeamCity is a client library targeting .netcore (.NET Platform 1.6) that provides an easy
way to interact with the [TeamCity API](https://confluence.jetbrains.com/display/TCD10/REST+API). 

# Getting started
To start using NetCoreTeamCity in your project, run the following command in the Package Manager Console:
```
Install-Package NetCoreTeamCity -Pre
```
Note this is still a beta, check first issues to see whats is already implemented and whats comming next.

# Using NetCoreTeamCity
Basic usage scenario would be getting detailed info of a build with known id:
```csharp
var teamCity = new TeamCity("host", "username", "password");
Build build = teamCity.Builds.Get(123);
```
Above code would return build located by build id 123

It is possible to find builds with search criteria, that are passed into query with locators, for example, to return builds from project named "projectName" that were run on "customBranch" and were added to the build queue no longer then a day from now:
```csharp
IList<Build> builds = teamCity.Builds.Find(
                By.Build
                    .QueuedDateAfter(DateTime.Now.AddDays(-1))
                    .Project("projectName")
                    .Branch("customBranch"));
```
Locators that can be used with given entity can be accessed from `By` class property, to provide easy and readable way of massing multiple search criteria to the api requests.

By default, when a list of entities is requested, only basic fields are included into the response. To include more then those, `Include` locator should be used that, same as for `By` locator, is accessed by static property. For example, with build:
```csharp
IList<Build> builds = teamCity.Builds.Find(
                        By.Build
                            .QueuedDateAfter(DateTime.Now.AddDays(-1)),
                            .Project("projectName")
                            .Branch("customBranch"),
                        Include.Build
                            .BuildType()
                            .Triggered()
                            .LastChanges()
                            .Agent()
                            .Properties());
```
Above example would return list of builds with BuildType, Triggered Date, LastChanges lisst, Agent the build was run on and list of Properties for the builds populated for each of returned build objects.

Currently queued builds are avaiable on another endpoint of TeamCity client:
```csharp
IList<Build> queuedBuilds = teamCity.QueuedBuilds.Find(Include.Build.QueuedDate());
```
Also, same `QueuedBuilds` endpoint is used for running new build (by adding it to a build queue):
```csharp
Build queuedBuild = teamCity.QueuedBuilds.Run("Build_Type_Id", comment: "Test build from API");
```
# Project state

Right know only basic build related call are beeing supported as I progress with the dev, more endpoints will be added. As it is my off-work project it will take some time until client will cover all avaiable API requests.
Also, right know I'm only targeting .NET Core as its using .NET Platform v1.6. Upon introduction of .NET Platform 2.0 this library will support both .NET Core and .NET 4.6.1 applications.
