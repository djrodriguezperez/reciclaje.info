using System.Text.Json.Serialization;

namespace Reciclaje.Info.Shared.Dto
{
  
    public class PuntosLimpiosFijosDto
    {  

        [JsonPropertyName("@graph")]
        public Graph[] _graph { get; set; }
    }

  

    public class Graph
    {
        [JsonPropertyName("@id")]
        public string _id { get; set; }
        [JsonPropertyName("@type")]
        public string _type { get; set; }
        public string id { get; set; }
        public string title { get; set; }
        public string relation { get; set; }
        public Address address { get; set; }
        public Location location { get; set; }
        public Organization organization { get; set; }
    }

    public class Address
    {
        public District district { get; set; }
        public Area area { get; set; }
        public string locality { get; set; }
        public string postalcode { get; set; }
        public string streetaddress { get; set; }
    }

    public class District
    {
        [JsonPropertyName("@id")]
        public string _id { get; set; }
    }

    public class Area
    {
        [JsonPropertyName("@id")]
        public string _id { get; set; }
    }

    public class Location
    {
        public float latitude { get; set; }
        public float longitude { get; set; }
    }

    public class Organization
    {
        public string organizationdesc { get; set; }
        public string accesibility { get; set; }
        public string schedule { get; set; }
        public string services { get; set; }
        public string organizationname { get; set; }
    }





}
