using GridIntersections.Models;
using System.Collections.Generic;

namespace GridIntersections.Services
{
    public interface IBasicAllCoordinatesGridService
    {
        BasicCoordinate GetClosestIntersection(List<string[]> wiresCommands);
        List<BasicCoordinate> GetCoordinates(GridMovementCommand[] commands);
    }
}