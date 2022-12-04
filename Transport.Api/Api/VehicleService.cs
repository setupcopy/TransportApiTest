using Transport.Api.Models;
using Newtonsoft.Json;

namespace Transport.Api.Api
{
    public class VehicleService
    {
        private HttpClient _client;

        public VehicleService(HttpClient client)
        {
            client.BaseAddress = new Uri("https://jayridechallengeapi.azurewebsites.net/");
            _client = client;
        }

        public async Task<TransportInfo> GetTransportInfo()
        {
            try
            {
                var response = await _client.GetAsync("/api/QuoteRequest");

                var result = new TransportInfo();

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var contentResponse = await response.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<TransportInfo>(contentResponse);

                return result;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
