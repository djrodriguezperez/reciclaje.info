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
            if (tipologia == PuntosLimpiosType.Fijos) // Json
            {
                var httpClient = _httpClientFactory.CreateClient();
                var result = await httpClient.GetFromJsonAsync<PuntosLimpiosFijosDto>(endpoint);
                return new JsonResult(result);
            }
            else // Geo 
            {
                //Uri endpoint = new Uri(BaseUriApi, _config[tipologia.ToString()]);
                return await invokeApiExterna(endpoint);
            }
           
        }

        //[HttpGet]
        //[Route("pl/proximidad")]
        //public async Task<ActionResult<GeoAtomDto>> GetPuntosLimpiosProximidadAsync()
        //{
        //    Uri endpoint = new Uri(BaseUriApi, _config["PuntosLimpiosProximidad"]);
        //    return await invokeApiExterna(endpoint);
        //}

        //[HttpGet]
        //[Route("pl/fijos/{filtro}")]
        //public async Task<ActionResult<PuntosLimpiosFijosDto>> GetPuntoLimpiosFijosAsync(string? filtro)
        //{

        //    HttpResponseMessage response;
        //    Uri endpoint = new Uri(BaseUriApi, _config["Fijos"]);
        //    string result = string.Empty;


        //    var httpClient = _httpClientFactory.CreateClient();
        //    response = await httpClient.GetAsync(endpoint);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        result = await response.Content.ReadAsStringAsync();
        //        result = result.NormalizarJson(); ///Extend Methods!
        //        return new JsonResult(result);
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }



        //}
        #endregion


        #region Equipamiento
        [HttpGet("equipamiento/{filtro}")]
        public async Task<ActionResult<IReadOnlyList<EquipamientoDto>>> GetEquipamientoAsync(string filtro)
        {
            string fileName = "Model/Tipos_Residuos.json";
            string jsonString = await System.IO.File.ReadAllTextAsync(fileName);
            IReadOnlyList<EquipamientoDto>? data = JsonSerializer.Deserialize<IReadOnlyList<EquipamientoDto>?>(jsonString);

            if (data != null && data.Any())
                return data.Where(t => t.Residuos.ToLowerInvariant().Contains(filtro.ToLowerInvariant())).ToList();
            else
            {
                return NotFound();
            }
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
            return await invokeApiExterna(endpoint);
        }
        //[HttpGet]
        //[Route("contenedores/pilas")]
        //public async Task<ActionResult<GeoAtomDto>> GetContendoresPilasAsync()
        //{

        //    Uri endpoint = new Uri(BaseUriApi, _config["ContenedoresPilasMarquesinas"]);
        //    return await invokeApiExterna(endpoint);
        //}
        //[HttpGet]
        //[Route("contenedores/aceite")]
        //public async Task<ActionResult<GeoAtomDto>> GetContendoresAceitesUsadoAsync()
        //{
        //    Uri endpoint = new Uri(BaseUriApi, _config["ContenedoresAceiteUsado"]);
        //    return await invokeApiExterna(endpoint);
        //}
        #endregion




        private async Task<ActionResult<GeoAtomDto>> invokeApiExterna(Uri endpoint)
        {
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
