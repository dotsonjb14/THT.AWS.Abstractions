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
            var options = Options.Create(new S3Options()
            {
                AwsProfile = "my_profile",
                ValidateHashes = true,
                LocalPath = "c:\\dev\\test_path\\"
            });


            await RunTest(new S3FileWrapper(options, credManager));
            await RunTest(new LocalFileWrapper(options));

            Console.Write("Press any key to exit...");

            Console.ReadKey();
        }

        static async Task RunTest(IFileWrapper fileWrapper)
        {
            var stringData = "Hello World\n";
            var bucket = "somestuff-jd";
            var key = "test.txt";

            await fileWrapper.WriteAsync(bucket, key, Encoding.UTF8.GetBytes(stringData));

            if(!await fileWrapper.ExistsAsync(bucket, key))
            {
                throw new Exception("file doesn't exist");
            }

            var data = await fileWrapper.ReadAsync(bucket, key);

            if(Encoding.UTF8.GetString(data) != stringData)
            {
                throw new Exception("data does not match");
            }

            await fileWrapper.DeleteAsync(bucket, key);

            if (await fileWrapper.ExistsAsync(bucket, key))
            {
                throw new Exception("file did not delete");
            }

            Console.WriteLine($"{fileWrapper.GetType().Name} is good :)");
        }
    }
}
