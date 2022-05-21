using Reciclaje.Info.Shared.Types;
using System.Net.Http.Json;

namespace Reciclaje.Info.Client.Services
{
	public class ContenedorService : IService<ContenedorType>
	{
		private const string BaseProxy = "api/v1/proxy/contenedores/"; // ToDo: Mover a settings		

		public ContenedorType Tipo { get ; set ; }
		public string? Descripcion { get ; set ; }
		public string? Endpoint { get; set ; }
		public string? Titulo { get ; set ; }
		public HttpClient? httpClient { get ; set ; }

		public ContenedorService(HttpClient client)
		{			
			Tipo = ContenedorType.Ropa; //Default
			httpClient = client;
			InicializarService();
		}
		public ContenedorService(HttpClient client, int id)
		{
			Tipo = (ContenedorType)id;
			httpClient = client;
			InicializarService();
		}


		private void InicializarService()
		{

			Endpoint = string.Format("{0}{1}", BaseProxy, Tipo.ToString());
			switch (Tipo)
			{
				case ContenedorType.AceiteUsado:
					Titulo = "Contenedores de aceite vegetal usado.";
					Descripcion = "En este conjunto de datos puede encontrar las direcciones, los horarios de recogida y los puntos geolocalizados de cada uno de los contenedores de aceite vegetal usado que están disponibles en los distritos..";
					break;

				case ContenedorType.PilasMarquesinas:
					Titulo = "Contenedores de pilas en marquesinas de autobús.";
					Descripcion = "Las pilas alcalinas/salinas y pilas de “botón” a desechar deben llevarse a un Punto Limpio móvil, ya que su presencia junto al resto de residuos puede resultar peligrosa.";
					break;

				case ContenedorType.Ropa:
					Titulo = "Contenedores de ropa autorizados del Ayuntamiento de Madrid.";
					Descripcion = "En este conjunto de datos puede encontrar las direcciones, los horarios de recogida y los puntos geolocalizados de cada uno de los contenedores de ropa usada que están ubicados en diversos puntos de la ciudad, en dependencias y mercadillos municipales, puntos limpios fijos, puntos limpios móviles y puntos limpios de proximidad.";
					break;
			}
		}


		public async Task<I?> GetDataAsync<I>()
		{
			if (Endpoint is null) throw new ArgumentNullException(nameof(Endpoint));
			return await httpClient!.GetFromJsonAsync<I>(Endpoint);

		}
	}
}
