using System.Text.Json.Serialization;

public class GeoAtomDto
{
    [JsonPropertyName("title")]
    public string title { get; set; }
    [JsonPropertyName("link")]
    public string link { get; set; }
    [JsonPropertyName("id")]
    public string id { get; set; }
    [JsonPropertyName("icon")]
    public string icon { get; set; }
    [JsonPropertyName("generator")]
    public string generator { get; set; }
    [JsonPropertyName("entries")]
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





