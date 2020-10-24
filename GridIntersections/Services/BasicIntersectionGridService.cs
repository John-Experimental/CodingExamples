using GridIntersections.Extensions;
using GridIntersections.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GridIntersections.Services
{
    public class BasicIntersectionGridService : IBasicIntersectionGridService
    {
        public IntersectionCoordinate GetClosestIntersection(List<string[]> wiresCommands)
        {
            var wiresCoordinates = new List<List<IntersectionCoordinate>>() { };

            foreach (var wireCommand in wiresCommands)
            {
                var GridMovementCommands = GetCommands(wireCommand);
                wiresCoordinates.Add(GetCoordinates(GridMovementCommands));
            }

            var intersections = GetIntersections(wiresCoordinates[0], wiresCoordinates[1]);

            return intersections.OrderBy(c => Math.Abs(c.XCoordinate) + Math.Abs(c.YCoordinate)).FirstOrDefault();
        }

        public List<IntersectionCoordinate> GetCoordinates(GridMovementCommand[] commands)
        {
            var coordinates = new List<IntersectionCoordinate>();

            var xCoordinate = 0;
            var yCoordinate = 0;
            var prevXCoordinate = 0;
            var prevYCoordinate = 0;
            bool isVerticalMovement;

            foreach (var command in commands)
            {
                switch (command.InstructionType)
                {
                    case 'U':
                        yCoordinate += command.Value;
                        isVerticalMovement = true;
                        break;
                    case 'D':
                        yCoordinate -= command.Value;
                        isVerticalMovement = true;
                        break;
                    case 'R':
                        xCoordinate += command.Value;
                        isVerticalMovement = false;
                        break;
                    case 'L':
                        xCoordinate -= command.Value;
                        isVerticalMovement = false;
                        break;
                    default:
                        throw new Exception();
                }

                coordinates.Add(new IntersectionCoordinate()
                {
                    XCoordinate = xCoordinate,
                    YCoordinate = yCoordinate,

                    PrevXCoordinate = prevXCoordinate,
                    PrevYCoordinate = prevYCoordinate,

                    IsVerticalMovement = isVerticalMovement
                });

                prevXCoordinate = xCoordinate;
                prevYCoordinate = yCoordinate;
            }

            return coordinates;
        }

        private GridMovementCommand[] GetCommands(string[] commands)
        {
            var resultCommands = new GridMovementCommand[commands.Length];

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

        private List<IntersectionCoordinate> GetIntersections(List<IntersectionCoordinate> wire1, List<IntersectionCoordinate> wire2)
        {
            var intersections = new List<IntersectionCoordinate>();

            foreach (var coordinate1 in wire1)
            {
                foreach (var coordinate2 in wire2)
                {
                    // Wires moving on the same axis will never cross each other on a coordinate closer than the first or last point
                    // ..both the first and the last point are also valid points for any subsequent opposite-axis-movement. Thus can be excluded
                    var movingOnSameAxis = coordinate1.IsVerticalMovement == coordinate2.IsVerticalMovement;

                    if (movingOnSameAxis == false)
                    {
                        IntersectionCoordinate verticalMover;
                        IntersectionCoordinate horizontalMover;

                        // The key to this method is to take 2 opposite moving instructions and compare their static axis with the moving axis of the other
                        // When coordinate1 moves on the Y axis, its X-axis will remain the same throughout the move. The reverse is also true for coordinate2's Y axis
                        if (coordinate1.IsVerticalMovement)
                        {
                            verticalMover = coordinate1;
                            horizontalMover = coordinate2;
                        }
                        else
                        {
                            verticalMover = coordinate2;
                            horizontalMover = coordinate1;
                        }

                        var crossingOnXAxis = verticalMover.XCoordinate.IsBetween(horizontalMover.XCoordinate, horizontalMover.PrevXCoordinate);
                        var crossingOnYAxis = horizontalMover.YCoordinate.IsBetween(verticalMover.YCoordinate, verticalMover.PrevYCoordinate);

                        if (crossingOnXAxis && crossingOnYAxis)
                        {
                            intersections.Add(new IntersectionCoordinate()
                            {
                                XCoordinate = coordinate1.IsVerticalMovement ? coordinate1.XCoordinate : coordinate2.XCoordinate,
                                YCoordinate = coordinate1.IsVerticalMovement ? coordinate2.YCoordinate : coordinate1.YCoordinate
                            });
                        }
                    }
                }
            }

            return intersections;
        }
    }
}
