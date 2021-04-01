namespace ProjectEarthServerAPI.Models.Buildplate
{
    public class BuildplateServerRequest
    {
        public double coordinateAccuracyVariance { get; set; }
        public PlayerCoordinate playerCoordinate { get; set; }
    }

    public class PlayerCoordinate
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
}