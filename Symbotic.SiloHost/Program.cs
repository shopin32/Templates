namespace Symbotic.SiloHost
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Grains;
    using Microsoft.Extensions.Logging;
    using Orleans;
    using Orleans.Configuration;
    using Orleans.Hosting;

    /// <summary>
    /// Orleans test silo host
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            var siloHost = new SiloHostBuilder()
                .UseLocalhostClustering(8000, 3500)
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "UniqueIdentifierToFindSilo";
                    options.ServiceId = "HelloWorldApp";
                })
                .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
                .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(Hello).Assembly).WithReferences())
                .ConfigureLogging(logging => logging.AddConsole())
                .Build();
            
            siloHost.StartAsync().Wait();

            Console.WriteLine("Press a button to stop silo");
            Console.ReadLine();
            
            siloHost.StopAsync().Wait();
        }
    }
}
