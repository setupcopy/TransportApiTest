namespace Transport.Api.Models
{
    public class TransportInfo
    {
        public string From { get; set; }

        public string To { get; set; }

        public List<Vehicle> listings { get; set; }
    }
}
