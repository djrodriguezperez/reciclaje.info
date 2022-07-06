using System.Text.Json.Serialization;

namespace Reciclaje.Info.Shared.Dto
{
    public class EquipamientosDto
    {
        public EquipamientosDto()
        {
            Equipamientos = new List<Equipamiento>();
        }
        [JsonPropertyName("Equipamientos")]
        public List<Equipamiento>? Equipamientos { get; set; }        
    }

    public class Equipamiento
    {
        [JsonPropertyName("Nombre")]
        public string? Nombre { get; set; }
        [JsonPropertyName("Avatar")]
        public string? Avatar { get; set; }
        [JsonPropertyName("Color")]
        public string? Color { get; set; }
        [JsonPropertyName("Equipamiento")]
        public string? Titulo { get; set; }
        [JsonPropertyName("Enlace")]
        public string? Enlace { get; set; }
        [JsonPropertyName("Residuos")]
        public string Residuos { get; set; } = "Sin Contenido";
    }
}
