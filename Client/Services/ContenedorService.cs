using MudBlazor;
using Reciclaje.Info.Shared.Types;
using System.Net.Http.Json;

namespace Reciclaje.Info.Client.Services
{
	public class ContenedorService : IGenericGeoService<ContenedorType>
	{
		private const string BaseProxy = "api/v1/proxy/contenedores/"; // ToDo: Mover a settings		

		public ContenedorType Tipo { get  ; set ; }
		public string? Descripcion { get ; set ; }
        public string? Icono { get; private set; }
        public string? Endpoint { get; set ; }
		public string? Titulo { get ; set ; }
		public HttpClient? httpClient { get ; set ; }

		public ContenedorService(HttpClient client)
		{				
			httpClient = client;			
		}
		
		public async Task<I?> GetDataAsync<I>(ContenedorType Tipo)
		{
			Endpoint = GetEndPoint(Tipo);
			if (Endpoint is null) throw new ArgumentNullException(nameof(Endpoint));
			return await httpClient!.GetFromJsonAsync<I>(Endpoint);

		}

        public string GetSubTitulo(ContenedorType Tipo)
        {
			var result = string.Empty;
            switch (Tipo)
            {
                case ContenedorType.AceiteUsado:                    
                    result = "En este conjunto de datos puede encontrar las direcciones, los horarios de recogida y los puntos geolocalizados de cada uno de los contenedores de aceite vegetal usado que están disponibles en los distritos..";
                    break;
                case ContenedorType.PilasMarquesinas:                    
                    result = "Las pilas alcalinas/salinas y pilas de “botón” a desechar deben llevarse a un Punto Limpio móvil, ya que su presencia junto al resto de residuos puede resultar peligrosa.";                    
                    break;
                case ContenedorType.Ropa:                    
                    result = "En este conjunto de datos puede encontrar las direcciones, los horarios de recogida y los puntos geolocalizados de cada uno de los contenedores de ropa usada que están ubicados en diversos puntos de la ciudad, en dependencias y mercadillos municipales, puntos limpios fijos, puntos limpios móviles y puntos limpios de proximidad.";                    
                    break;
            }
			return result;
        }

		public string GetTitulo(ContenedorType Tipo)
        {
			var result = string.Empty;
			switch (Tipo)
			{
				case ContenedorType.AceiteUsado:
					result = "Contenedores de aceite vegetal usado.";					
					break;

				case ContenedorType.PilasMarquesinas:
					result = "Contenedores de pilas en marquesinas de autobús.";					
					break;

				case ContenedorType.Ropa:
					result = "Contenedores de ropa autorizados del Ayuntamiento de Madrid.";					
					break;
			}
			return result;
		}

        public string GetIcono(ContenedorType Tipo)
        {
			var result = string.Empty;
			switch (Tipo)
			{
				case ContenedorType.AceiteUsado:					
					result = Icons.Material.Filled.DeleteForever;
					break;
				case ContenedorType.PilasMarquesinas:					
					result = Icons.Material.Filled.BatterySaver;
					break;
				case ContenedorType.Ropa:					
					result = Icons.Material.Filled.Checkroom;
					break;
			}
			return result;
		}

        public string GetEndPoint(ContenedorType Tipo)
        {
			return string.Format("{0}{1}", BaseProxy, Tipo.ToString());
		}
    }
}
