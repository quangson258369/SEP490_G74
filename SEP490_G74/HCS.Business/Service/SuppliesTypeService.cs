using AutoMapper;
using HCS.Business.Pagination;
using HCS.Business.RequestModel.SuppliesTypeRequestModel;
using HCS.Business.ResponseModel.ApiRessponse;
using HCS.Business.ResponseModel.SuppliesTypeResponseModel;
using HCS.DataAccess.UnitOfWork;
using HCS.Domain.Models;

namespace HCS.Business.Service;

public interface ISuppliesTypeService
{
    Task<ApiResponse> GetSuppliesType(int suppliesTypeId);
    Task<ApiResponse> GetAllSuppliesType();
    Task<ApiResponse> AddSuppliesType(SuppliesTypeAddModel suppliesType);
    Task<ApiResponse> UpdateSuppliesType(int suppliesTypeId, SuppliesTypeUpdateModel suppliesType);
    Task<ApiResponse> DeleteSuppliesType(int suppliesTypeId);
}

public class SuppliesTypeService : ISuppliesTypeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SuppliesTypeService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ApiResponse> GetSuppliesType(int suppliesTypeId)
    {
        var response = new ApiResponse();

        var suppliesTypeEntity =
            await _unitOfWork.SuppliesTypeRepo.GetAsync(entry => entry.SuppliesTypeId == suppliesTypeId);

        if (suppliesTypeEntity is null)
        {
            return response.SetNotFound($"Supplies Type Not Found with Id {suppliesTypeId}");
        }

        var itemResponse = _mapper.Map<SuppliesTypeResponseModel>(suppliesTypeEntity);
        return response.SetOk(itemResponse);
    }

    public async Task<ApiResponse> GetAllSuppliesType()
    {
        var response = new ApiResponse();
        var items = await _unitOfWork.SuppliesTypeRepo.GetAllAsync(entry => true);

        var itemsResponse = _mapper.Map<List<SuppliesTypeResponseModel>>(items).Paginate(1,5);

        return response.SetOk(itemsResponse);
    }

    public async Task<ApiResponse> AddSuppliesType(SuppliesTypeAddModel suppliesType)
    {
        var response = new ApiResponse();

        var mapSuppliesType = _mapper.Map<SuppliesType>(suppliesType);

        var currentItem =
            await _unitOfWork.SuppliesTypeRepo.GetAsync(entry =>
                entry.SuppliesTypeName == suppliesType.SuppliesTypeName);

        if (currentItem is not null)
        {
            throw new Exception("Supplies Type already existed.");
        }

        await _unitOfWork.SuppliesTypeRepo.AddAsync(mapSuppliesType);
        await _unitOfWork.SaveChangeAsync();

        return response.SetOk("Created");
    }

    public async Task<ApiResponse> UpdateSuppliesType(int suppliesTypeId, SuppliesTypeUpdateModel suppliesType)
    {
        var response = new ApiResponse();

        var currentItem = await _unitOfWork.SuppliesTypeRepo.GetAsync(entry => entry.SuppliesTypeId == suppliesTypeId);

        if (currentItem is null)
        {
            return response.SetNotFound($"Supplies Type Not Found with Id {suppliesTypeId}");
        }

        currentItem.SuppliesTypeName = suppliesType.SuppliesTypeName;
        await _unitOfWork.SaveChangeAsync();

        return response.SetOk("Updated");
    }

    public async Task<ApiResponse> DeleteSuppliesType(int suppliesTypeId)
    {
        var response = new ApiResponse();
        var currentItem = await _unitOfWork.SuppliesTypeRepo.GetAsync(entry => entry.SuppliesTypeId == suppliesTypeId);

        if (currentItem is null)
        {
            return response.SetNotFound($"Supplies Type Not Found with Id {suppliesTypeId}");
        }

        await _unitOfWork.SuppliesTypeRepo.RemoveByIdAsync(suppliesTypeId);
        await _unitOfWork.SaveChangeAsync();

        return response.SetOk("Deleted");
    }
}