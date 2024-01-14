using HCS.API.Controllers;
using HCS.Business.ResponseModel.ApiResponse;
using HCS.Business.ResponseModel.CategoryResponse;
using HCS.Business.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public async Task GetCategory_ReturnsOkResult()
        {
            // Arrange
            int categoryId = 1;
            var mockCategoryService = new Mock<ICategoryService>();
            mockCategoryService.Setup(service => service.GetCategory(categoryId))
                              .ReturnsAsync(new ApiResponse().SetOk(new CategoryResponseModel()));

            var controller = new CategoryController(mockCategoryService.Object);

            // Act
            var result = await controller.GetCategory(categoryId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var apiResponse = Assert.IsType<ApiResponse>(okResult.Value);
            Assert.True(apiResponse.IsSuccess);
        }

        [Fact]
        public async Task GetCategories_ReturnsOkResult()
        {
            // Arrange
            var mockCategoryService = new Mock<ICategoryService>();
            mockCategoryService.Setup(service => service.GetCategories())
                              .ReturnsAsync(new ApiResponse().SetOk(new CategoryResponseModel[] { }));

            var controller = new CategoryController(mockCategoryService.Object);

            // Act
            var result = await controller.GetCategories();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var apiResponse = Assert.IsType<ApiResponse>(okResult.Value);
            Assert.True(apiResponse.IsSuccess);
        }
    }
}