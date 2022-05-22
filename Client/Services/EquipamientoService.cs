using MudBlazor;
using Reciclaje.Info.Shared.Dto;
using System.Net.Http.Json;


namespace Reciclaje.Info.Client.Services
{
    public class EquipamientoService : IEquipamientoService
    {
        private string BaseProxy = "api/v1/proxy/"; // ToDo: Mover a settings

        public EquipamientoService(HttpClient client)
        {
            httpClient = client;
            InicializarService();
        }

        private void InicializarService()
        {

            Endpoint = string.Format("{0}{1}/", BaseProxy, "equipamiento");
            Titulo = "Tipos de residuos y donde depositarlos.";
            SubTitulo = "Relación de residuos con identificación del tipo de equipamiento o contenedor donde se pueden depositar para hacer su recogida selectiva.";
            Icono = Icons.Material.Filled.Compost;

        }

        public string? SubTitulo { get; set; }
        public string? Icono { get;  set; }
        public string? Endpoint { get; set; }
        public string? Titulo { get; set; }

    
        public HttpClient? httpClient { get; set; }

        public async Task<EquipamientosDto?> GetDataAsync(string filtro)
        {
            if (Endpoint is null) throw new ArgumentNullException(nameof(Endpoint));
            return await httpClient!.GetFromJsonAsync<EquipamientosDto>(Endpoint + filtro);
        }
    }
}
