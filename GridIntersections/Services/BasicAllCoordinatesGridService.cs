using GridIntersections.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GridIntersections.Services
{
    public class BasicAllCoordinatesGridService : IBasicAllCoordinatesGridService
    {
        public BasicCoordinate GetClosestIntersection(List<string[]> wiresCommands)
        {
            var wiresCoordinates = new List<List<BasicCoordinate>>() { };

            foreach (var wireCommand in wiresCommands)
            {
                var gridCommands = GetCommands(wireCommand);

                wiresCoordinates.Add(GetCoordinates(gridCommands));
            }

            var intersections = GetIntersections(wiresCoordinates[0], wiresCoordinates[1]);

            // Math.Abs returns the absolute value of an integer (basically removes the sign). E.g. -1 == 1 && 1 == 1
            // Using this to order the intersections based on their total distance from the point of origin
            return intersections.OrderBy(c => Math.Abs(c.XCoordinate) + Math.Abs(c.YCoordinate)).FirstOrDefault();
        }

        public List<BasicCoordinate> GetCoordinates(GridMovementCommand[] commands)
        {
            var coordinates = new List<BasicCoordinate>();

            var xCoordinate = 0;
            var yCoordinate = 0;

            foreach (var command in commands)
            {
                // Create a separate coordinate for every coordinate touched by the wire
                for (int i = 1; i <= command.Value; i++)
                {
                    switch (command.InstructionType)
                    {
                        case 'U':
                            yCoordinate++;
                            break;
                        case 'D':
                            yCoordinate--;
                            break;
                        case 'R':
                            xCoordinate++;
                            break;
                        case 'L':
                            xCoordinate--;
                            break;
                        default:
                            throw new Exception();
                    }

                    coordinates.Add(new BasicCoordinate()
                    {
                        XCoordinate = xCoordinate,
                        YCoordinate = yCoordinate
                    });
                }
            }
            return coordinates;
        }

        private GridMovementCommand[] GetCommands(string[] commands)
        {
            var resultCommands = new GridMovementCommand[commands.Length];

            // Separates the InstructionType and Value of all commands and adds them to an array
            for (int i = 0; i < commands.Length; i++)
            {
                resultCommands[i] = new GridMovementCommand()
                {
                    InstructionType = commands[i].ToCharArray()[0],
                    Value = int.Parse(commands[i].Substring(1))
                };
            }

            return resultCommands;
        }

        private List<BasicCoordinate> GetIntersections(List<BasicCoordinate> wire1, List<BasicCoordinate> wire2)
        {
            var intersections = new List<BasicCoordinate>();

            foreach (var firstCoordinate in wire1)
            {
                foreach (var secondCoordinate in wire2)
                {
                    if (firstCoordinate.XCoordinate == secondCoordinate.YCoordinate &&
                        firstCoordinate.YCoordinate == secondCoordinate.YCoordinate)
                    {
                        intersections.Add(firstCoordinate);
                    }
                }
            }

            // Possible alternative with linq statements
            // Under water this linq statement will perform something similar to the double foreach above
            /* 
                var intersecionss = wire1.Where(coordinate =>
                wire2.Any(sc => coordinate.XCoordinate == sc.XCoordinate && coordinate.YCoordinate == sc.YCoordinate))
                .ToList();
            */

            return intersections;
        }
    }
}
