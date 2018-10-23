using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using THT.AWS.Abstractions.Credentials;
using THT.AWS.Abstractions.Options;
using THT.AWS.Abstractions.S3;

namespace tester
{
    class Program
    {
        static void Main(string[] args)
        {
            doStuff().Wait();
        }

        static async Task doStuff()
        {
            var credManager = new CrendentialsManager();
            var x = new S3FileWrapper(Options.Create(new S3Options() {
                AwsProfile = "my_profile",
                ValidateHashes = false
            }), credManager);

            await x.WriteAsync("somestuff-jd", "test.txt", Encoding.UTF8.GetBytes("Hello World\n"));
            var data = await x.ReadAsync("somestuff-jd", "test.txt");

            Console.Write(Encoding.UTF8.GetString(data));
            Console.ReadKey();
        }
    }
}
