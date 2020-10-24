using GridIntersections.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GridIntersections.Services
{
    public class TestService : ITestService
    {
        public List<string[]> CreateFakeGridCommandsLists(int numberOfItemsPerList)
        {
            var rng = new Random();

            var coordinatesLists = new List<string[]>();

            // Creates 2 lists with equal amount of commands (as per input) with movement values between 1-1000.
            // Subsequent for loops iterate with += 2 to ensure Up/Down and Right/Left alternate
            for (int i = 0; i < 2; i++)
            {
                var commands = new string[numberOfItemsPerList];

                // This for loop creates all the Up and Down commands
                for (int j = 0; j < numberOfItemsPerList; j += 2)
                {
                    var commandTypeNumber = rng.Next(0, 2);
                    var command = commandTypeNumber == 0 ? new StringBuilder("U", 5) : new StringBuilder("D", 5);
                    command.Append(rng.Next(1, 1001));

                    commands[j] = command.ToString();
                }

                // This for loop creates all the Right and Left commands
                for (int k = 1; k < numberOfItemsPerList; k += 2)
                {
                    var commandTypeNumber = rng.Next(0, 2);
                    var command = commandTypeNumber == 0 ? new StringBuilder("L", 5) : new StringBuilder("R", 5);
                    command.Append(rng.Next(1, 1001));

                    commands[k] = command.ToString();
                }

                coordinatesLists.Add(commands);
            }

            return coordinatesLists;
        }
    }
}
