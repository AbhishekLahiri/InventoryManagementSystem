using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using InventoryManagementSystem.Api.Controllers;
using InventoryManagementSystem.Api.Services;
using InventoryManagementSystem.Shared.DTOs;

namespace InventoryManagementSystem.Tests
{
    public class InventoryControllerTests
    {
        private readonly Mock<IInventoryService> _mockService;
        private readonly InventoryController _controller;

        public InventoryControllerTests()
        {
            _mockService = new Mock<IInventoryService>();
            _controller = new InventoryController(_mockService.Object);
        }

        // =========================================================================
        // 1. GET ALL ITEMS TESTS
        // =========================================================================

        [Fact]
        public async Task GetAllItems_WhenItemsExist_ReturnsOkWithData()
        {
            // Arrange
            var fakeDbItems = new List<InventoryItemDto>
            {
                new() { Id = 1, Sku = "SKU-001", Name = "Laser Head", Quantity = 10, UnitPrice = 500m },
                new() { Id = 2, Sku = "SKU-MED-002", Name = "Telemetry Sensor", Quantity = 45, UnitPrice = 85.50m }
            };
            _mockService.Setup(s => s.GetAllItemsAsync()).ReturnsAsync(fakeDbItems);

            // Act
            var result = await _controller.GetAllItems();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedItems = Assert.IsType<List<InventoryItemDto>>(okResult.Value);
            Assert.Equal(2, returnedItems.Count);
        }

        // =========================================================================
        // 2. GET BY ID TESTS
        // =========================================================================

        [Fact]
        public async Task GetItem_WhenItemExists_ReturnsOkWithItem()
        {
            // Arrange
            int itemId = 42;
            var fakeItem = new InventoryItemDto { Id = itemId, Sku = "SKU-042", Name = "Sensor" };
            _mockService.Setup(s => s.GetItemByIdAsync(itemId)).ReturnsAsync(fakeItem);

            // Act
            var result = await _controller.GetItem(itemId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedItem = Assert.IsType<InventoryItemDto>(okResult.Value);
            Assert.Equal(itemId, returnedItem.Id);
            Assert.Equal("SKU-042", returnedItem.Sku);
        }

        [Fact]
        public async Task GetItem_WhenItemDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            int missingId = 999;
            _mockService.Setup(s => s.GetItemByIdAsync(missingId)).ReturnsAsync((InventoryItemDto?)null);

            // Act
            var result = await _controller.GetItem(missingId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        // =========================================================================
        // 3. CREATE ITEM (POST) TESTS
        // =========================================================================

        [Fact]
        public async Task CreateItem_ValidPayload_ReturnsCreatedAtActionWithData()
        {
            // Arrange
            var inputDto = new InventoryItemDto { Sku = "NEW-SKU", Name = "New Part", Quantity = 5 };
            var outputDto = new InventoryItemDto { Id = 101, Sku = "NEW-SKU", Name = "New Part", Quantity = 5 };

            _mockService.Setup(s => s.CreateItemAsync(inputDto)).ReturnsAsync(outputDto);

            // Act
            var result = await _controller.CreateItem(inputDto);

            // Assert
            // Asserting CreatedAtAction (HTTP 201)
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(nameof(_controller.GetItem), createdResult.ActionName);
            Assert.Equal(101, createdResult.RouteValues?["id"]);

            var returnedItem = Assert.IsType<InventoryItemDto>(createdResult.Value);
            Assert.Equal(101, returnedItem.Id);
        }

        // =========================================================================
        // 4. UPDATE ITEM (PUT) TESTS
        // =========================================================================

        [Fact]
        public async Task UpdateItem_WhenItemExists_ReturnsOkWithUpdatedJson()
        {
            // Arrange
            int targetId = 5;
            var inputUpdateDto = new UpdateItemDto { Sku = "UPD-SKU", Name = "Updated Name", CategoryId = 2, CategoryName = "Diagnostics" };
            var returnedUpdatedDto = new InventoryItemDto { Id = targetId, Sku = "UPD-SKU", Name = "Updated Name", CategoryId = 2, CategoryName = "Diagnostics" };

            _mockService.Setup(s => s.UpdateItemAsync(targetId, inputUpdateDto)).ReturnsAsync(returnedUpdatedDto);

            // Act
            var result = await _controller.UpdateItem(targetId, inputUpdateDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var dataValue = Assert.IsType<InventoryItemDto>(okResult.Value);
            Assert.Equal("Updated Name", dataValue.Name);
            Assert.Equal("Diagnostics", dataValue.CategoryName); // Verifies rich UI contract projection works
        }

        [Fact]
        public async Task UpdateItem_WhenItemDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            int missingId = 999;
            var inputUpdateDto = new UpdateItemDto { Sku = "FAIL" };
            _mockService.Setup(s => s.UpdateItemAsync(missingId, inputUpdateDto)).ReturnsAsync((InventoryItemDto?)null);

            // Act
            var result = await _controller.UpdateItem(missingId, inputUpdateDto);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        // =========================================================================
        // 5. DELETE ITEM TESTS
        // =========================================================================

        [Fact]
        public async Task DeleteItem_WhenItemExists_ReturnsNoContent()
        {
            // Arrange
            int targetId = 10;
            _mockService.Setup(s => s.DeleteItemAsync(targetId)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteItem(targetId);

            // Assert
            Assert.IsType<NoContentResult>(result); // HTTP 204
        }

        [Fact]
        public async Task DeleteItem_WhenItemDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            int missingId = 999;
            _mockService.Setup(s => s.DeleteItemAsync(missingId)).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteItem(missingId);

            // Assert
            Assert.IsType<NotFoundResult>(result); // HTTP 404
        }
    }
}