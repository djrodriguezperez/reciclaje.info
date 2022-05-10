using System.Text.Json.Serialization;

public class GeoAtomDto
{
    public string title { get; set; }
    public string link { get; set; }
    public string id { get; set; }
    public string icon { get; set; }
    public string generator { get; set; }
    public IEnumerable<Entry> entries { get; set; }
}




public class Entry
{
    public string? title { get; set; }
    public object? id { get; set; }

    [JsonPropertyName("georss:point")]
    public string? georsspoint { get; set; }

    [JsonPropertyName("geo:lat")]
    public string? geolat { get; set; }
    [JsonPropertyName("geo:long")]
    public string? geolong { get; set; }
}





