using InventoryManagementSystem.Shared.DTOs;
using InventoryManagementSystem.Shared.Models;
using System.Net.Http.Json;

namespace InventoryManagementSystem.UI.Services
{
    public class InventoryServiceClient
    {
        private readonly HttpClient _http;

        public InventoryServiceClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<InventoryItemDto>> GetItemsAsync()
        {
            return await _http.GetFromJsonAsync<List<InventoryItemDto>>("api/inventory/items")
                   ?? new List<InventoryItemDto>();
        }

        public async Task<InventoryItemDto?> GetItemByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<InventoryItemDto>($"api/inventory/items/{id}");
        }

        public async Task<InventoryItemDto?> UpdateItemAsync(int id, UpdateItemDto itemDto)
        {
            var response = await _http.PutAsJsonAsync($"api/inventory/items/{id}", itemDto);

            if (response.IsSuccessStatusCode)
            {
                // Deserialize and return the shiny new JSON object!
                return await response.Content.ReadFromJsonAsync<InventoryItemDto>();
            }

            return null; // Handle failure as needed
        }

        public async Task<List<CategoryDto>> GetCategoriesAsync()
        {
            return await _http.GetFromJsonAsync<List<CategoryDto>>("api/inventory/categories")
                ?? new List<CategoryDto>();
        }

        public async Task<HttpResponseMessage> CreateItemAsync(CreateItemDto itemDto)
        {
            return await _http.PostAsJsonAsync("api/inventory/items", itemDto);
        }

        public async Task<HttpResponseMessage> DeleteItemAsync(int id)
        {
            return await _http.DeleteAsync($"api/inventory/items/{id}");
        }
    }
}