using System;
using MaxMind.GeoIP2;

namespace ConsoleApplication27
{
    class Program
    {
        static void Main(string[] args)
        {
            // install-package MaxMind.GeoIP2
            var reader = new DatabaseReader("GeoLite2-Country.mmdb");
            var response = reader.Country("8.8.8.8");
            Console.WriteLine(response.Country.IsoCode);
        }
    }
}
