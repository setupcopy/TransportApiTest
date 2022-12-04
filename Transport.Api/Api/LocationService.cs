using Newtonsoft.Json;
using Transport.Api.Models;

namespace Transport.Api.Api
{
    public class LocationService
    {
        private HttpClient _client;
        private string _accessKey = "8d317ca3749e0700f12f859e0236f3aa"; //TODO: Add the key into configruation file

        public LocationService(HttpClient client)
        {
            client.BaseAddress = new Uri("http://api.ipstack.com/");
            _client = client;
        }

        public async Task<string> GetLocation()
        {
            try
            {
                var response = await _client.GetAsync($"/check?access_key={_accessKey}&fields=city");

                if (!response.IsSuccessStatusCode)
                {
                    return string.Empty;
                }

                var contentResponse = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<Location>(contentResponse);

                return result.City;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
