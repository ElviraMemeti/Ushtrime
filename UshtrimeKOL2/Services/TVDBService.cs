using SOA2024.MovieReview.API.ExternalModels;
using System.Net.Http.Headers;
using System.Text.Json;

namespace UshtrimeKOL2.Services
{
    public class TVDBService : ITVDBServise
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "afac2dc0-d5bb-48ab-81d0-a36cafe9854c";
        private readonly string _token;

        public TVDBService(HttpClient httpClient)

        {
            _httpClient = httpClient;

            _token = GetAuthToken().Result;
        }



        private async Task<string> GetAuthToken()
        {
            var requestBody = new
            {
                _apiKey = _apiKey,
            };

            var requestContent = new StringContent(JsonSerializer.Serialize(requestBody), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://api4.thetvdb.com/v4/login", requestContent);

            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return tokenResponse.Data.Token;

        }



        public async Task<List<TVDBMovie>> GetTopRatedMoviesAsync()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await _httpClient.GetAsync($"https://api4.thetvdb.com/v4/movies?page=1");
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var moviesResult = JsonSerializer.Deserialize<TVDBMoviesResult>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return moviesResult.Data;
        }
    }
}
