using BrowserInterop.Extensions;
using BrowserInterop.Geolocation;
using Microsoft.JSInterop;

namespace Reciclaje.Info.Client.Services
{
    public class GeolocationService : IGeolocationService
    {
        IJSRuntime jsRuntime;
        private WindowNavigatorGeolocation? geolocationWrapper;
        private GeolocationResult? currentPosition;
        /// <summary>
        /// Consctructor  (Injección de Dependencias:  JSRuntime)
        /// </summary>
        /// <param name="jSRuntime"></param>
        public GeolocationService(IJSRuntime jSRuntime)
        {
            jsRuntime = jSRuntime;
        }
        /// <summary>
        /// Método asíncrono que obtiene el objeto windows del navegador 
        /// Invoca el método: GetCurrentPosition() 
        /// </summary>
        /// <returns></returns>
        public async Task<GeolocationResult> GetCurrentPosition()
        {

            var window = await jsRuntime.Window();
            var navigator = await window.Navigator();
            geolocationWrapper = navigator.Geolocation;
            currentPosition = (await geolocationWrapper.GetCurrentPosition(new PositionOptions()
            {
                EnableHighAccuracy = false,
                MaximumAgeTimeSpan = TimeSpan.FromHours(1),
                TimeoutTimeSpan = TimeSpan.FromMinutes(1)
            }));
            return currentPosition;
        }

    }
}
