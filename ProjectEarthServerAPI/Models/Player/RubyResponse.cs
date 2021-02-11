namespace ProjectEarthServerAPI.Models
{
    public class RubyResponse
    {
        public int result { get; set; }
    }

    public class SplitRubyResponse
    {
        public SplitRubyResult result { get; set; }
    }

    public class SplitRubyResult
    {
        public int purchased { get; set; }
        public int earned { get; set; }
    }
}