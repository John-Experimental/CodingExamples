using System.Collections.Generic;

namespace GridIntersections.Services.Interfaces
{
    public interface ITestService
    {
        List<string[]> CreateFakeGridCommandsLists(int numberOfItemsPerList);
    }
}