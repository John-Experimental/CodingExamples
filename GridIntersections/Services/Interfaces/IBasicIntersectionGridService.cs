using GridIntersections.Models;
using System.Collections.Generic;

namespace GridIntersections.Services
{
    public interface IBasicIntersectionGridService
    {
        IntersectionCoordinate GetClosestIntersection(List<string[]> wiresCommands);
        List<IntersectionCoordinate> GetCoordinates(GridMovementCommand[] commands);
    }
}