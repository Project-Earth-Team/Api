namespace ProjectEarthServerAPI.Models.Buildplate
{
    public class BuildplateServerRequest
    {
        public double coordinateAccuracyVariance { get; set; }
        public Coordinate playerCoordinate { get; set; }
    }

}