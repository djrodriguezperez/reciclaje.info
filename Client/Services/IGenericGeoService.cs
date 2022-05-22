namespace Reciclaje.Info.Client.Services
{
	public interface IGenericGeoService<T>
	{		
		T Tipo { get; set; }
	
		HttpClient? httpClient { get; set; }
		Task<I?> GetDataAsync<I>(T Tipo);
        string GetTitulo(T Tipo);
		string GetSubTitulo(T Tipo);
		string GetIcono(T Tipo);
		string GetEndPoint(T Tipo);
		
	}
}