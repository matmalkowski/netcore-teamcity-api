using System;
using NetCoreTeamCity.Api;

namespace NetCoreTeamCity.Samples
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var teamCity = new TeamCity(args[0], args[1], args[2]);
            var b = teamCity.GetBuild();
            Console.WriteLine(b.ToString());
            Console.ReadLine();
        }
    }
}
