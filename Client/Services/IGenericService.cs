using Reciclaje.Info.Shared.Types;

namespace Reciclaje.Info.Client.Services
{
	public interface IService<T>
	{		
		T Tipo { get; set; }
		string? Descripcion { get; set; }
		string? Endpoint { get; set; }	
		string? Titulo { get; set; }
		HttpClient? httpClient { get; set; }
		Task<I?> GetDataAsync<I>() ;
	}
}