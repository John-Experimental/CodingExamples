namespace GridIntersections.Models
{
    public class IntersectionCoordinate
    {
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public int PrevXCoordinate { get; set; }
        public int PrevYCoordinate { get; set; }
        public bool IsVerticalMovement { get; set; }
    }
}
