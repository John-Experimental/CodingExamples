using GridIntersections.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GridIntersections.Services
{
    public class RunApplication : IRunApplication
    {
        private readonly IBasicAllCoordinatesGridService _basicAllCoordinatesGridService;
        private readonly ITestService _testService;
        private readonly IBasicIntersectionGridService _basicIntersectionGridService;

        public RunApplication(IBasicAllCoordinatesGridService basicAllCoordinatesGridService, ITestService testService, IBasicIntersectionGridService basicIntersectionGridService)
        {
            _basicAllCoordinatesGridService = basicAllCoordinatesGridService;
            _testService = testService;
            _basicIntersectionGridService = basicIntersectionGridService;
        }

        public void Run()
        {
            var inputValuePairs = RequestInput();
            var gridCommands = _testService.CreateFakeGridCommandsLists(inputValuePairs["NumberOfCommands"]);

            var timeEntries = new List<TimeSpan>();

            for (int i = 1; i <= inputValuePairs["NumberOfRuns"]; i++)
            {
                var sw = new Stopwatch();
                sw.Start();
                // You'll have to change the service if you want to test the original solution
                var coordinate = _basicIntersectionGridService.GetClosestIntersection(gridCommands);
                sw.Stop();

                timeEntries.Add(sw.Elapsed);
            }

            Console.WriteLine($"It took an average of {timeEntries.Average(t => t.TotalSeconds)} seconds to get the closest intersection");

            Console.ReadLine();
        }

        private Dictionary<string, int> RequestInput()
        {
            return new Dictionary<string, int>()
            {
                {"NumberOfCommands", RequestNumberOfCommands() },
                {"NumberOfRuns", RequestNumberOfRuns() }
            };
        }

        private int RequestNumberOfCommands()
        {
            Console.WriteLine("How many commands should be in your lists?:");
            var isInputValid = int.TryParse(Console.ReadLine(), out int numberOfCommands);

            if (isInputValid == false)
            {
                Console.WriteLine("Input was invalid, try again.");
                numberOfCommands = RequestNumberOfCommands();
            }

            return numberOfCommands;
        }

        private int RequestNumberOfRuns()
        {
            Console.WriteLine("How many times should the analysis be executed:");
            var isInputValid = int.TryParse(Console.ReadLine(), out int numberOfRuns);

            if (isInputValid == false)
            {
                Console.WriteLine("Input was invalid, try again.");
                numberOfRuns = RequestNumberOfRuns();
            }

            return numberOfRuns;
        }
    }
}
