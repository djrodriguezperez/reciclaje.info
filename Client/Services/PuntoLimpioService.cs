using Reciclaje.Info.Shared.Types;
using System.Net.Http.Json;

namespace Reciclaje.Info.Client.Services
{
    public class PuntoLimpioService : IService<PuntosLimpiosType>
    {
        private const string BaseProxy = "api/v1/proxy/pl/"; // ToDo: Mover a settings

		public PuntoLimpioService(HttpClient client)
		{
			Tipo = PuntosLimpiosType.Fijos; //Default
			httpClient = client;
			InicializarService();
		}

		public PuntoLimpioService(HttpClient client, int id)
		{
			Tipo = (PuntosLimpiosType)id;
			httpClient = client;
			InicializarService();
		}



		private void InicializarService()
		{

			Endpoint = string.Format("{0}{1}", BaseProxy, Tipo.ToString());
			switch (Tipo)
			{
				case PuntosLimpiosType.Fijos:
					Titulo = "Puntos Limpios Fijos.";
					Descripcion = "Información de datos, ubicación, características, horarios, coordenadas de localización y servicios de los distintos puntos limpios fijos municipales de la ciudad de Madrid..";
					break;

				case PuntosLimpiosType.Proximidad:
					Titulo = "Puntos Limpios de Proximidad.";
					Descripcion = "En estos  Puntos Limpios de Proximidad, se pueden depositar, para su posterior reutilización y reciclaje: aceite vegetal usado (se deberá depositar en una botella de plástico), tapones de botellas, libros y revistas, residuos de aparatos eléctricos y electrónicos de pequeño tamaño, pilas y baterías usadas, fluorescentes y bombillas de bajo consumo, cartuchos de tóner y aerosoles, cápsulas de café, radiografías, y CD's, DVD's y cintas de vídeo.";
					break;

				case PuntosLimpiosType.Moviles:
					Titulo = "Puntos Limpios Móviles.";
					Descripcion = "Información de datos, ubicación, características, horarios y dias disponibles, coordenadas de localización y servicios de los distintos puntos limpios móviles municipales de la ciudad de Madrid.";
					break;
			}
		}


		public PuntosLimpiosType Tipo { get ; set ; }
		public string? Descripcion { get ; set ; }
		public string? Endpoint { get ; set ; }
		public string? Titulo { get ; set ; }
		public HttpClient? httpClient { get ; set ; }

		public async Task<I?> GetDataAsync<I>()
		{
			if (Endpoint is null) throw new ArgumentNullException(nameof(Endpoint));
			return await httpClient!.GetFromJsonAsync<I>(Endpoint);
		}
	}
}
