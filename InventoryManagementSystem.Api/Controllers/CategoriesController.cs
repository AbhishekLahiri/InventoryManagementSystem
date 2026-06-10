using Microsoft.AspNetCore.Mvc;
using InventoryManagementSystem.Api.Services;
using InventoryManagementSystem.Shared.DTOs;

namespace InventoryManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("api/inventory/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly IInventoryService _service;

        public CategoriesController(IInventoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var categories = await _service.GetCategoriesAsync();
            return Ok(categories);
        }
    }
}