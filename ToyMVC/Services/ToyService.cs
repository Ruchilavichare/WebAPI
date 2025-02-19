using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ToyMVC.Models;

namespace ToyMVC.Services
{
    public class ToyService
    {
        private readonly HttpClient _httpClient;
        private const string ApiBaseUrl = "https://localhost:7040/api/toys";

        public ToyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Toy>> GetHighConvertingToysAsync()
        {
            var response = await _httpClient.GetAsync($"{ApiBaseUrl}/high-converting");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Toy>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return new List<Toy>();
        }

        public async Task<List<Toy>> GetTopRatedToysAsync()
        {
            var response = await _httpClient.GetAsync($"{ApiBaseUrl}/top-rated");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Toy>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return new List<Toy>();
        }

        public async Task<List<Toy>> GetToysByCategoryAsync(string category)
        {
            var response = await _httpClient.GetAsync($"{ApiBaseUrl}/best-for/{category}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Toy>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return new List<Toy>();
        }

        public async Task<List<Toy>> GetDealsOfTheDayAsync()
        {
            var response = await _httpClient.GetAsync($"{ApiBaseUrl}/deals-of-the-day");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Toy>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return new List<Toy>();
        }
    }
}
