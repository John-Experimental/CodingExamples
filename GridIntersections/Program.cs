using GridIntersections.Services;
using GridIntersections.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GridIntersections
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IRunApplication, RunApplication>()
                .AddSingleton<IBasicAllCoordinatesGridService, BasicAllCoordinatesGridService>()
                .AddSingleton<IBasicIntersectionGridService, BasicIntersectionGridService>()
                .AddSingleton<ITestService, TestService>()
                .BuildServiceProvider();

            var program = serviceProvider.GetService<IRunApplication>();
            program.Run();
        }
    }
}
