using Nominatim.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Nominatim.Clients
{
    public class NominatimClient : INominatimClient
    {
        private const string BaseUri = "https://nominatim.openstreetmap.org/search?q=";
        private const string Format = "&format=jsonv2";

        private readonly HttpClient _httpClient;

        public NominatimClient()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "C# NominatimClient");
        }

        public async Task<RequestResponseModel> SearchAsync(StructuredQuerySearchModel searchModel)
        {
            try
            {
                // Build the request URL with the query
                string requestUrl = $"{BaseUri}{searchModel}{Format}";

                // Send the GET request and get the response
                HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);

                return response.IsSuccessStatusCode
                    ? await HandleSuccessfulResponseAsync(response)
                    : HandleErrorResponse(response);
            }
            catch (Exception ex)
            {
                // Handle any unexpected exceptions
                return new RequestResponseModel
                {
                    Status = Status.Error,
                    ErrorMessage = $"Unexpected error: {ex.Message}"
                };
            }
        }

        private async Task<RequestResponseModel> HandleSuccessfulResponseAsync(HttpResponseMessage response)
        {
            var apiResponse = await DeserializeResponseAsync<List<ApiResponseModel>>(response);

            return new RequestResponseModel
            {
                Status = Status.Success,
                ApiResponse = apiResponse ?? new List<ApiResponseModel>()
            };
        }

        private RequestResponseModel HandleErrorResponse(HttpResponseMessage response)
        {
            return new RequestResponseModel
            {
                Status = Status.Error,
                ErrorMessage = $"Error: {response.StatusCode} - {response.ReasonPhrase}"
            };
        }

        private async Task<T?> DeserializeResponseAsync<T>(HttpResponseMessage response) where T : class
        {
            try
            {
                // Read the content of the response and deserialize it
                string content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(content);
            }
            catch (JsonException ex)
            {
                // Handle JSON deserialization errors
                throw new InvalidOperationException($"Error deserializing JSON: {ex.Message}", ex);
            }
        }
    }
}
