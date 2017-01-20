using System;
using NetCoreTeamCity.Api;

namespace NetCoreTeamCity.Samples
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var teamCity = new TeamCity(args[0], args[1], args[2]);
            //var b = teamCity.Builds.Get(412910);
            var b = teamCity.Builds.Find(By.Build.QueuedDateAfter(DateTime.Now.AddDays(-1)), Include.Build.BuildType().Triggered().LastChanges().Agent().Properties());
            Console.WriteLine(b.ToString());
            Console.ReadLine();
        }
    }
}
