using System.Net;

namespace Retail.API.Servicos
{
    internal class VehicleServiceCaller : IVehicleServiceCaller
    {
        private readonly HttpClient _httpClient;

        public VehicleServiceCaller(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> VehicleExists(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/veiculos/{id}");

                if (response.StatusCode == HttpStatusCode.NotFound)
                    return false;

                response.EnsureSuccessStatusCode();
                 return true;
            }
            catch (Exception)
            {
                throw new Exception("Vehicle service unavailable");
            }
        }
    }
}
