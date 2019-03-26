using System;
using System.Threading.Tasks;
using RestSharp;

namespace Checkmarx.Soccer.FootballData
{
    public class FootballDataApi
    {

        private readonly RestClient _client;

        public FootballDataApi(FootballDataSettings settings)
        {
            _client = new RestClient(settings.BaseUrl);
            _client.AddDefaultHeader("X-Auth-Token", settings.ApiKey);
        }


        public async Task<T> Execute<T>(RestRequest request) where T : new()
        {
            var response = await _client.ExecuteTaskAsync<T>(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var footballDataException = new ApplicationException(message, response.ErrorException);
                throw footballDataException;
            }
            return response.Data;
        }
    }
}
