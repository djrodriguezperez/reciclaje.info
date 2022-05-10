using System.Text.Json.Serialization;

namespace Reciclaje.Info.Shared.Dto
{
  
    public class PuntosLimpiosFijosDto
    {  

        [JsonPropertyName("@graph")]
        public Graph[] _graph { get; set; }
    }

    //public class Context
    //{
    //    public string c { get; set; }
    //    public string dcterms { get; set; }
    //    public string geo { get; set; }
    //    public string loc { get; set; }
    //    public string org { get; set; }
    //    public string vcard { get; set; }
    //    public string schema { get; set; }
    //    public string title { get; set; }
    //    public string id { get; set; }
    //    public string relation { get; set; }
    //    public string references { get; set; }
    //    public string address { get; set; }
    //    public string area { get; set; }
    //    public string district { get; set; }
    //    public string locality { get; set; }
    //    public string postalcode { get; set; }
    //    public string streetaddress { get; set; }
    //    public string location { get; set; }
    //    public string latitude { get; set; }
    //    public string longitude { get; set; }
    //    public string organization { get; set; }
    //    public string organizationdesc { get; set; }
    //    public string accesibility { get; set; }
    //    public string services { get; set; }
    //    public string schedule { get; set; }
    //    public string organizationname { get; set; }
    //    public string description { get; set; }
    //    public string link { get; set; }
    //    public string uid { get; set; }
    //    public string dtstart { get; set; }
    //    public string dtend { get; set; }
    //    public string time { get; set; }
    //    public string excludeddays { get; set; }
    //    public string eventlocation { get; set; }
    //    public string free { get; set; }
    //    public string price { get; set; }
    //    public string recurrence { get; set; }
    //    public string days { get; set; }
    //    public string frequency { get; set; }
    //    public string interval { get; set; }
    //    public string audience { get; set; }
    //}

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
