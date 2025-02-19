
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using HotelManagementSystem.Models;

namespace HotelManagementSystem.Services
{
    public class MenuApiService
    {
        private readonly HttpClient _httpClient;

        public MenuApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Menu>> GetMenusAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Menu>>("api/menu");
        }

        public async Task<Menu> GetMenuAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Menu>($"api/menu/{id}");
        }

        public async Task CreateMenuAsync(Menu menu)
        {
            await _httpClient.PostAsJsonAsync("api/menu", menu);
        }

        public async Task UpdateMenuAsync(int id, Menu menu)
        {
            await _httpClient.PutAsJsonAsync($"api/menu/{id}", menu);
        }

        public async Task DeleteMenuAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/menu/{id}");
        }
    }
}
