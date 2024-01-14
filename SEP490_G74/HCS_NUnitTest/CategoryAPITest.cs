using NUnit.Framework;
using Moq;
using AutoMapper;
using HCS.Business.Pagination;
using HCS.Business.RequestModel.CategoryRequestModel;
using HCS.Business.ResponseModel.ApiResponse;
using HCS.Business.ResponseModel.CategoryResponse;
using HCS.DataAccess.UnitOfWork;
using HCS.Domain.Models;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HCS.Business.Service;

[TestFixture]
public class CategoryServiceTests
{
    private Mock<IUnitOfWork> _mockUnitOfWork;
    private Mock<IMapper> _mockMapper;
    private CategoryService _categoryService;

    [SetUp]
    public void Setup()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockMapper = new Mock<IMapper>();
        _categoryService = new CategoryService(_mockUnitOfWork.Object, _mockMapper.Object);
    }

    [Test]
    public async Task GetCategory_ValidId_ReturnsOkResult()
    {
        // Arrange
        int categoryId = 1;
        var categoryEntity = new Category { CategoryId = categoryId, /* other properties */ };
        var categoryResponseModel = new CategoryResponseModel { CategoryId = categoryId, /* other properties */ };
        var apiResponse = new ApiResponse().SetOk(categoryResponseModel);

        _mockUnitOfWork.Setup(uow => uow.CategoryRepo.GetAsync(It.IsAny<Expression<Func<Category, bool>>>()))
                       .ReturnsAsync(categoryEntity);
        _mockMapper.Setup(mapper => mapper.Map<CategoryResponseModel>(categoryEntity))
                   .Returns(categoryResponseModel);

        // Act
        var result = await _categoryService.GetCategory(categoryId);

        // Assert
        Assert.AreEqual(apiResponse.StatusCode, result.StatusCode);
        Assert.AreEqual(apiResponse.IsSuccess, result.IsSuccess);
        Assert.AreEqual(apiResponse.Result, result.Result);
    }

    [Test]
    public async Task GetCategory_InvalidId_ReturnsNotFoundResult()
    {
        // Arrange
        int invalidCategoryId = -1;
        var apiResponse = new ApiResponse().SetNotFound($"Category Not Found With ID {invalidCategoryId}");

        _mockUnitOfWork.Setup(uow => uow.CategoryRepo.GetAsync(It.IsAny<Expression<Func<Category, bool>>>()))
                       .ReturnsAsync((Category)null);

        // Act
        var result = await _categoryService.GetCategory(invalidCategoryId);

        // Assert
        Assert.AreEqual(apiResponse.StatusCode, result.StatusCode);
        Assert.AreEqual(apiResponse.IsSuccess, result.IsSuccess);
        Assert.AreEqual(apiResponse.Result, result.Result);
    }

    // Add similar tests for GetCategories, AddCategory, UpdateCategory, and DeleteCategory actions
}
