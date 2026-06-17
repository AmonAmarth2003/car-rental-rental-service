using System.Net;

namespace Retail.API.Servicos
{
    internal class ClientServiceCaller : IClientServiceCaller
    {
        private readonly HttpClient _httpClient;

        public ClientServiceCaller(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> ClientExists(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"clients/{id}");

                if (response.StatusCode == HttpStatusCode.NotFound)
                    return false;

                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception)
            {
                throw new Exception("Client service unavailable");
            }
        }
    }
}
