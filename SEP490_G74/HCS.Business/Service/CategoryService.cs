using AutoMapper;
using HCS.Business.Pagination;
using HCS.Business.RequestModel.CategoryRequestModel;
using HCS.Business.ResponseModel.ApiRessponse;
using HCS.Business.ResponseModel.CategoryResponse;
using HCS.DataAccess.UnitOfWork;
using HCS.Domain.Models;

namespace HCS.Business.Service;

public interface ICategoryService
{
    Task<ApiResponse> GetCategory(int categoryId);
    Task<ApiResponse> GetCategories();
    Task<ApiResponse> AddCategory(CategoryAddModel category);
    Task<ApiResponse> UpdateCategory(int categoryId, CategoryUpdateModel category);
    Task<ApiResponse> DeleteCategory(int categoryId);
}

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ApiResponse> GetCategory(int categoryId)
    {
        var response = new ApiResponse();

        var category = await _unitOfWork.CategoryRepo.GetAsync(entry => entry.CategoryId == categoryId);

        if (category is null)
        {
             return response.SetNotFound($"Category Not Found With ID {categoryId}");
        }

        var categoryResponse = _mapper.Map<CategoryResponseModel>(category);

        return response.SetOk(categoryResponse);
        
    }

    public async Task<ApiResponse> GetCategories()
    {
        var response = new ApiResponse();

        var categories = await _unitOfWork.CategoryRepo.GetCategories();

        var categoriesResponse = _mapper.Map<List<CategoryResponseModel>>(categories);
        
        return response.SetOk(categoriesResponse);
    }

    public async Task<ApiResponse> AddCategory(CategoryAddModel category)
    {
        var response = new ApiResponse();
        var categoryEntity = _mapper.Map<Category>(category);

        await _unitOfWork.CategoryRepo.AddAsync(categoryEntity);
        await _unitOfWork.SaveChangeAsync();

        return response.SetOk("Created");
    }

    public async Task<ApiResponse> UpdateCategory(int categoryId, CategoryUpdateModel category)
    {
        var response = new ApiResponse();

        var categoryEntity = await _unitOfWork.CategoryRepo.GetAsync(entry => entry.CategoryId == categoryId);

        if (categoryEntity is null) return response.SetNotFound($"Category Not Found with Id {categoryId}");

        categoryEntity.CategoryName = category.CategoryName;
        await _unitOfWork.SaveChangeAsync();

        return response.SetOk("Updated");
    }

    public async Task<ApiResponse> DeleteCategory(int categoryId)
    {
        var response = new ApiResponse();

        var categoryEntity = await _unitOfWork.CategoryRepo.GetAsync(entry => entry.CategoryId == categoryId);

        if (categoryEntity is null) return response.SetNotFound($"Category Not Found with Id {categoryId}");
        
        await _unitOfWork.CategoryRepo.RemoveByIdAsync(categoryId);
        await _unitOfWork.SaveChangeAsync();
        
        return response.SetOk("Deleted");
    }
}