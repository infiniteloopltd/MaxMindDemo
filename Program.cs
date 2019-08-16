using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using MaxMind.GeoIP2;

namespace ConsoleApplication27
{
    class Program
    {
        static void Main(string[] args)
        {
            var fi = new FileInfo("GeoLite2-Country.mmdb");
            if (!fi.Exists || (DateTime.Now - fi.LastWriteTime).TotalDays > 30)
            {
                DownloadGeoliteDB();
            }
            var reader = new DatabaseReader("GeoLite2-Country.mmdb");
            var response = reader.Country("8.8.8.8");
            Console.WriteLine(response.Country.IsoCode);
        }

        private static void DownloadGeoliteDB()
        {
            var wc = new WebClient();
            var bData = wc.DownloadData("https://geolite.maxmind.com/download/geoip/database/GeoLite2-Country.tar.gz");
            var zippedStream = new MemoryStream(bData);
            var files = Tar.ExtractTarGzEntries(zippedStream);
            var db = files.First(zippedFile => zippedFile.FileName.EndsWith("mmdb"));
            File.WriteAllBytes("GeoLite2-Country.mmdb", db.Contents);
        }

    }
}
