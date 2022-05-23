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
        public GeolocationService(IJSRuntime jSRuntime)
        {
            jsRuntime = jSRuntime;
        }
        public async Task<GeolocationResult> GetCurrentPosition()
        {

            var window = await jsRuntime.Window();
            var navigator = await window.Navigator();
            geolocationWrapper = navigator.Geolocation;
            currentPosition = (await geolocationWrapper.GetCurrentPosition(new PositionOptions()
            {
                EnableHighAccuracy = true,
                MaximumAgeTimeSpan = TimeSpan.FromHours(1),
                TimeoutTimeSpan = TimeSpan.FromMinutes(1)
            }));
            return currentPosition;
        }

    }
}
