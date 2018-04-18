namespace Symbotic.Client
{
    using System;
    using System.Threading.Tasks;
    using GrainInterfaces;
    using Microsoft.Extensions.Logging;
    using Orleans;
    using Orleans.Configuration;
    using Orleans.Runtime;

    class Program
    {
        static void Main(string[] args)
        {
            using (var client = GetCLient().Result)
            {
                var helloService = client.GetGrain<IHello>(Guid.NewGuid());
    
                var result = helloService.SayHello("World").Result;
    
                Console.WriteLine(result);
            }
        }

        private static async Task<IClusterClient> GetCLient()
        {
            var client =  new ClientBuilder()
                .UseLocalhostClustering(3500)
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "UniqueIdentifierToFindSilo";
                    options.ServiceId = "HelloWorldApp";
                })
                .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(IHello).Assembly).WithReferences())
                .ConfigureLogging(logging => logging.AddConsole())
                .Build();

            await client.Connect();
            return client;
        }
    }
}
