using InventoryManagementSystem.Shared.DTOs;

namespace InventoryManagementSystem.Api.Services
{
    public interface IInventoryService
    {
        Task<IEnumerable<InventoryItemDto>> GetAllItemsAsync();
        Task<InventoryItemDto?> GetItemByIdAsync(int id);
        Task<InventoryItemDto> CreateItemAsync(InventoryItemDto itemDto);
        Task<InventoryItemDto?> UpdateItemAsync(int id, UpdateItemDto itemDto);
        Task<bool> DeleteItemAsync(int id);
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
    }
}