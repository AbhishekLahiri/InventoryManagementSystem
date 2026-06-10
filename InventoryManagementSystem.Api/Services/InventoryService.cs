using InventoryManagementSystem.Api.Data;
using InventoryManagementSystem.Shared.Models;
using InventoryManagementSystem.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Api.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly AppDbContext _context;

        // DbContext is injected directly here into the service layer
        public InventoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InventoryItemDto>> GetAllItemsAsync()
        {
            return await _context.InventoryItems
                .Select(item => new InventoryItemDto
                {
                    Id = item.Id,
                    Sku = item.SKU,
                    Name = item.Name,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    CategoryName = item.Category != null ? item.Category.Name : "Unassigned",
                })
                .ToListAsync();
        }

        public async Task<InventoryItemDto?> GetItemByIdAsync(int id)
        {
            var item = await _context.InventoryItems
                .Include(i => i.Category)
                .FirstOrDefaultAsync(i => i.Id == id);
            if (item == null) return null;

            return new InventoryItemDto
            {
                Id = item.Id,
                Sku = item.SKU,
                Name = item.Name,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                CategoryName = item.Category != null ? item.Category.Name : "Unassigned"
            };
        }

        public async Task<InventoryItemDto> CreateItemAsync(InventoryItemDto itemDto)
        {
            var entity = new InventoryItem
            {
                SKU = itemDto.Sku,
                Name = itemDto.Name,
                Quantity = itemDto.Quantity,
                UnitPrice = itemDto.UnitPrice,
                CategoryId = itemDto.CategoryId,
                LastUpdated = DateTime.UtcNow, // set timestamp
            };

            _context.InventoryItems.Add(entity);
            await _context.SaveChangesAsync();

            itemDto.Id = entity.Id; // Assign database-generated identity back
            return itemDto;
        }

        public async Task<InventoryItemDto?> UpdateItemAsync(int id, UpdateItemDto itemDto)
        {
            var existingItem = await _context.InventoryItems
                .Include(i => i.Category)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (existingItem == null) return null;

            existingItem.SKU = itemDto.Sku;
            existingItem.Name = itemDto.Name;
            existingItem.Quantity = itemDto.Quantity;
            existingItem.UnitPrice = itemDto.UnitPrice;
            existingItem.CategoryId = itemDto.CategoryId;
            existingItem.Category = itemDto.CategoryId != 0 ? await _context.Categories.FindAsync(itemDto.CategoryId) : null;
            existingItem.LastUpdated = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            var updatedDto = new InventoryItemDto
            {
                Id = existingItem.Id,
                Sku = existingItem.SKU,
                Name = existingItem.Name,
                Quantity = existingItem.Quantity,
                UnitPrice = existingItem.UnitPrice,
                CategoryId = existingItem.CategoryId,
                CategoryName = itemDto.CategoryName
                //CategoryName = existingItem.Category != null ? existingItem.Category.Name : "Unassigned"
            };

            return updatedDto;
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var entity = await _context.InventoryItems.FindAsync(id);
            if (entity == null) return false;

            _context.InventoryItems.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            return await _context.Categories
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }
    }
}
