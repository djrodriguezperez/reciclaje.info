using System.Text.Json.Serialization;

namespace Reciclaje.Info.Shared.Dto
{
    public class EquipamientosDto
    {
        [JsonPropertyName("Equipamientos")]
        public IEnumerable<Equipamiento>? Equipamientos { get; set; }        
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
        [JsonPropertyName("Residuos")]
        public string Residuos { get; set; } = "Sin Contenido";
    }
}
