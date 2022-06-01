using Microsoft.AspNetCore.Mvc;
using Reciclaje.Info.Shared.Dto;
using Reciclaje.Info.Shared.Types;
using Reciclaje.Info.Shared.Utils;

using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Reciclaje.Info.Server.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProxyController : ControllerBase
    {
        private readonly ILogger<ProxyController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;
        private readonly Uri BaseUriApi;

        const string BaseUriParam = "BaseApiOpenData";

        /// <summary>
        /// Constructor Proxy con DI 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="httpClientFactory"></param>
        public ProxyController(ILogger<ProxyController> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _config = configuration;
            BaseUriApi = new Uri(_config[BaseUriParam]);
        }

        #region Puntos Limpios
        [HttpGet]
        [Route("pl/{tipologia}/{filtro?}", Name ="")]
        public async Task<ActionResult<GeoAtomDto>> GetPuntosLimpiosAsync(PuntosLimpiosType? tipologia, string? filtro)
        {
            if (tipologia == null) throw new ArgumentNullException(nameof(tipologia));
            Uri endpoint = new Uri(BaseUriApi, _config[tipologia.ToString()]);
            if (tipologia == PuntosLimpiosType.Fijos) // Caso de Uso Especial - Invoca la API y devuelve objeo Json
            {
                var httpClient = _httpClientFactory.CreateClient();
                var result = await httpClient.GetFromJsonAsync<PuntosLimpiosFijosDto>(endpoint);
                return new JsonResult(result);
            }
            else // Geo 
            {               
                return await GetOpenData(endpoint);
            }
           
        }
        #endregion

        #region Equipamiento
        /// <summary>
        /// Metodo Get para la obtención de datos de equipamiento según los datos filtrados.     
        /// </summary>
        /// <param name="tipologia">Valores: Ropa, AceiteUsado, Pilas Marquesinas </param>
        /// <returns> ActionResult<GeoAtomDto> </returns>
        [HttpGet("equipamiento/{filtro}")]
        public async Task<ActionResult<EquipamientosDto>> GetEquipamientoAsync(string filtro)
        {
            if (string.IsNullOrWhiteSpace(filtro))
            {
                throw new ArgumentException($"\"{nameof(filtro)}\" no puede ser NULL ni un espacio en blanco.", nameof(filtro));
            }

            string jsonFile = _config["Equipamiento"]; // Procesar Previamente
            string jsonString = await System.IO.File.ReadAllTextAsync(jsonFile);
            EquipamientosDto? data = JsonSerializer.Deserialize<EquipamientosDto>(jsonString.NormalizarJson());
            EquipamientosDto resultDto = new EquipamientosDto();
                                                   
            resultDto = Buscar(filtro, data)!;


            return resultDto;
            
    
        }
        /// <summary>
        /// Algoritmo de búqueda en catálogo de equipamiento.
        /// </summary>
        /// <param name="filtro"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private static EquipamientosDto? Buscar(string filtro, EquipamientosDto? data)
        {
            if (data == null || string.IsNullOrWhiteSpace(filtro)) return null;
            var result = new EquipamientosDto();


            /// 1º Buscamos contenido literal en título (prevalece a residuos).
            foreach (var item in data.Equipamientos!)
			{
                if (item.Titulo!.ToLowerInvariant().Contains(filtro.ToLowerInvariant()))
                {
                    result.Equipamientos!.Add(item);
                } 
			}
            /// 2º Si no existe resultados, realizamos una búsqueda en contenido de residuos admitidos.
            if (result.Equipamientos!.Any() == false)
            {
                foreach (var item in data.Equipamientos!)
                {
                    if (item.Residuos.ToUpperInvariant().Contains(filtro.ToUpperInvariant()))
                    {
                        if (result.Equipamientos!.Contains(item) == false)
                        {
                            result.Equipamientos.Add(item);
                        }
                    }
                }

            }           
            return result;
        }
        #endregion

        #region Contenedores
        /// <summary>
        /// Metodo Get para la obtención de datos de geolocalización de los contenedores según tipología.     
        /// </summary>
        /// <param name="tipologia">Valores: Ropa, AceiteUsado, Pilas Marquesinas </param>
        /// <returns> ActionResult<GeoAtomDto> </returns>
        [HttpGet]
        [Route("contenedores/{tipologia}")]
        public async Task<ActionResult<GeoAtomDto>> GetContendoresAsync(ContenedorType?  tipologia)
        {
            if (tipologia == null) throw new ArgumentNullException(nameof(tipologia));
            Uri endpoint = new Uri(BaseUriApi, _config[tipologia.ToString()]);
            return await GetOpenData(endpoint);
        }
        
        #endregion


        private async Task<ActionResult<GeoAtomDto>> GetOpenData(Uri? endpoint)
        {
            if (endpoint == null) throw new ArgumentNullException(nameof(endpoint));
            try
            {
                var geoResult = await GeoParserCustom.DeserializeGeoFeedToDtoAsync(endpoint);

                if (geoResult != null)
                    return Ok(geoResult);
                else
                    return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        

    }
}
