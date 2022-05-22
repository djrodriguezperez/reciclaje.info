using MudBlazor;
using Reciclaje.Info.Shared.Types;
using System.Net.Http.Json;


namespace Reciclaje.Info.Client.Services
{
    public class PuntoLimpioService : IGenericGeoService<PuntosLimpiosType>
    {
        private const string BaseProxy = "api/v1/proxy/pl/"; // ToDo: Mover a settings

		public PuntoLimpioService(HttpClient client)
		{		
			httpClient = client;			
		}
		
		public PuntosLimpiosType Tipo { get ; set ; }
		public string? Endpoint { get; set; }
		public HttpClient? httpClient { get ; set ; }

		public async Task<I?> GetDataAsync<I>(PuntosLimpiosType Tipo)
		{
			Endpoint = GetEndPoint(Tipo);
			if (Endpoint is null) throw new ArgumentNullException(nameof(Endpoint));
			return await httpClient!.GetFromJsonAsync<I>(Endpoint);
		}


        public string GetTitulo(PuntosLimpiosType Tipo)
        {
			var result = string.Empty;
			switch (Tipo)
			{
				case PuntosLimpiosType.Fijos:
					result = "Puntos Limpios Fijos.";
					break;
				case PuntosLimpiosType.Proximidad:
					result = "Puntos Limpios de Proximidad.";
					break;
				case PuntosLimpiosType.Moviles:
					result = "Puntos Limpios Móviles.";
					break;
			}
			return result;
		}

        public string GetSubTitulo(PuntosLimpiosType Tipo)
        {
			var result = string.Empty;
			switch (Tipo)
			{
				case PuntosLimpiosType.Fijos:
					result = "Información de datos, ubicación, características, horarios, coordenadas de localización y servicios de los distintos puntos limpios fijos municipales de la ciudad de Madrid..";
					break;
				case PuntosLimpiosType.Proximidad:
					result = "En estos  Puntos Limpios de Proximidad, se pueden depositar, para su posterior reutilización y reciclaje: aceite vegetal usado (se deberá depositar en una botella de plástico), tapones de botellas, libros y revistas, residuos de aparatos eléctricos y electrónicos de pequeño tamaño, pilas y baterías usadas, fluorescentes y bombillas de bajo consumo, cartuchos de tóner y aerosoles, cápsulas de café, radiografías, y CD's, DVD's y cintas de vídeo.";
					break;
				case PuntosLimpiosType.Moviles:
					result = "Información de datos, ubicación, características, horarios y dias disponibles, coordenadas de localización y servicios de los distintos puntos limpios móviles municipales de la ciudad de Madrid.";
					break;
			}
			return result;
		}

        public string GetIcono(PuntosLimpiosType Tipo)
        {
			var result = string.Empty;
			switch (Tipo)
			{
				case PuntosLimpiosType.Fijos:
					result = Icons.Material.Filled.LocationOn;
					break;
				case PuntosLimpiosType.Proximidad:
					result = Icons.Material.Filled.LocationSearching;
					break;
				case PuntosLimpiosType.Moviles:
					result = Icons.Material.Filled.EditLocation;
					break;
			}
			return result;
		}

        public string GetEndPoint(PuntosLimpiosType Tipo)
        {
			return string.Format("{0}{1}", BaseProxy, Tipo.ToString());
		}
    }
}
