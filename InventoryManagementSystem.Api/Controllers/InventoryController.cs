using Microsoft.AspNetCore.Mvc;
using InventoryManagementSystem.Api.Services;
using InventoryManagementSystem.Shared.DTOs;

namespace InventoryManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("api/inventory/items")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _service;

        public InventoryController(IInventoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryItemDto>>> GetAllItems()
        {
            var items = await _service.GetAllItemsAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryItemDto>> GetItem(int id)
        {
            var item = await _service.GetItemByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<InventoryItemDto>> CreateItem(InventoryItemDto itemDto)
        {
            var createdItem = await _service.CreateItemAsync(itemDto);
            return CreatedAtAction(nameof(GetItem), new { id = createdItem.Id }, createdItem);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<InventoryItemDto>> UpdateItem(int id, UpdateItemDto itemDto)
        {
            var updatedItem = await _service.UpdateItemAsync(id, itemDto);

            if (updatedItem == null)
                return NotFound();
            return Ok(updatedItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var success = await _service.DeleteItemAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}