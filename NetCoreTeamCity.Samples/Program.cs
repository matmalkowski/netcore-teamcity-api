using System;
using NetCoreTeamCity.Api;

namespace NetCoreTeamCity.Samples
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var teamCity = new TeamCity(args[0], args[1], args[2]);

            var builds = teamCity.Builds.Find(
                By.Build.QueuedDateAfter(DateTime.Now.AddDays(-1)),
                Include.Build
                    .BuildType()
                    .Triggered()
                    .LastChanges()
                    .Agent()
                    .Properties());


            var queuedBuilds = teamCity.QueuedBuilds.Find(Include.Build.QueuedDate());

            var queuedBuild = teamCity.QueuedBuilds.Run("Build_Type_Id", comment: "Test build from API");

            Console.ReadLine();
        }
    }
}
