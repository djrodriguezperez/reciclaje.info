using BrowserInterop.Geolocation;

namespace Reciclaje.Info.Client.Services
{
    public interface IGeolocationService
    {
        Task<GeolocationResult> GetCurrentPosition();
    }
}