namespace GridIntersections.Extensions
{
    public static class Comparisons
    {
        public static bool IsBetween(this int value, int first, int second)
        {
            int lowerBound;
            int upperBound;

            if (first < second)
            {
                lowerBound = first;
                upperBound = second;
            }
            else
            {
                lowerBound = second;
                upperBound = first;
            }

            return value > lowerBound && value < upperBound;
        }
    }
}
