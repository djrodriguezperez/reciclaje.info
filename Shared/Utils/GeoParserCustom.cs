using Reciclaje.Info.Shared.Dto;
using System.Xml.Linq;

namespace Reciclaje.Info.Shared.Utils
{
    /// <summary>
    /// Parser personalizado para la deserialización del microformato utilizado para el marcado de coordenadas 
    /// geográficas (GeoRSS-Simple) en el portal de datos abiertos de Ayto. Madrid.
    /// El formato GeoRSS-Simple está basado en la especificación estandar ATOM https://www.w3.org/2005/Atom.
    /// A partir de la URL del recuros GEO, realizar una lectura mediante LINQ-XML de los elementos en un objeto C#.
    /// </summary>
    public class GeoParserCustom
    {
        private static readonly object _lock = new object();
        public static GeoAtomDto DeserializeGeoFeedToDto(Uri url)
        {
            lock (_lock)
            {
                try
                {
                    XDocument? doc = XDocument.Load(url.AbsoluteUri);
                    GeoAtomDto _geo = new GeoAtomDto();
                    XNamespace nsa = "http://www.w3.org/2005/Atom";
                    XNamespace nsgeorss = "http://www.georss.org/georss";
                    XNamespace nsgeo = "http://www.w3.org/2003/01/geo/wgs84_pos#";

                    // Cabecera Feed
                    _geo.title = (string)(from e in doc.Descendants(nsa + "title") select e).First();
                    _geo.generator = (string)(from e in doc.Descendants(nsa + "generator") select e).First();
                    _geo.link = (string)(from e in doc.Descendants(nsa + "link") select e.Attribute("href")).First();
                    _geo.id = (string)(from e in doc.Descendants(nsa + "id") select e).First();
                    _geo.icon = (string)(from e in doc.Descendants(nsa + "icon") select e).First();
                    // Entradas Feed
                    var entries = doc.Descendants(nsa + "entry")
                       .Select(item => new Entry()
                       {
                           title = (string)item.Element(nsa + "title")!,
                           link = System.Web.HttpUtility.HtmlDecode((string)item.Element(nsa + "link")!.Attribute("href")!),
                           content = System.Web.HttpUtility.HtmlDecode((string)item.Element(nsa + "content")!),
                           georsspoint = (string)item.Element(nsgeorss + "point")!,
                           geolat = (string)item.Element(nsgeo + "lat")!,
                           geolong = (string)item.Element(nsgeo + "long")!,
                       }).ToList();


                    _geo.entries = entries;
                    return _geo;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }


        public async static Task<GeoAtomDto> DeserializeGeoFeedToDtoAsync(Uri url) { 

            return await Task.Run(() => DeserializeGeoFeedToDto(url));
        }
    }
}
