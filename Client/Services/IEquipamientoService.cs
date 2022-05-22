using Reciclaje.Info.Shared.Dto;

namespace Reciclaje.Info.Client.Services
{
    public interface IEquipamientoService
    {
        string? Endpoint { get; set; }
        HttpClient? httpClient { get; set; }
        string? Icono { get; set; }
        string? SubTitulo { get; set; }
        string? Titulo { get; set; }

        Task<EquipamientosDto?> GetDataAsync(string filtro);
    }
}