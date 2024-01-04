using AutoMapper;
using HCS.Business.RequestModel.ServiceTypeRequestModel;
using HCS.Business.ResponseModel.ApiResponse;
using HCS.Business.ResponseModel.ServiceTypeResponseModel;
using HCS.DataAccess.UnitOfWork;
using HCS.Domain.Models;

namespace HCS.Business.Service;

public interface IServiceTypeService
{
    Task<ApiResponse> GetServiceType(int serviceTypeId);
    Task<ApiResponse> GetListServiceType(int categoryId);
    Task<ApiResponse> AddServiceType(ServiceTypeAddModel serviceTypeAddModel);
    Task<ApiResponse> UpdateServiceType(int serviceTypeId, ServiceTypeUpdateModel serviceTypeUpdateModel);
    Task DeleteServiceType(int serviceTypeId);
    Task<ApiResponse> GetListServiceByServiceTypeId(int serviceTypeId);
}
public class ServiceTypeService: IServiceTypeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ServiceTypeService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ApiResponse> GetServiceType(int serviceTypeId)
    {
        var response = new ApiResponse();

        var entity = await _unitOfWork.ServiceTypeRepo.GetAsync(x => x.ServiceTypeId == serviceTypeId);

        if (entity is null)
        {
            return response.SetNotFound($"Not Found ServiceType with Id {serviceTypeId}");
        }

        var serviceTypeResponse = _mapper.Map<ServiceTypeResponseModel>(entity);

        return response.SetOk(serviceTypeResponse);
    }

    public async Task<ApiResponse> GetListServiceType(int categoryID)
    {
        var response = new ApiResponse();

        var listEntity = await _unitOfWork.ServiceTypeRepo.GetAllAsync(x => x.CategoryId == categoryID);

        var entityResponse = _mapper.Map<List<ServiceTypeResponseModel>>(listEntity);

        return response.SetOk(entityResponse);
    }

    public async Task<ApiResponse> AddServiceType(ServiceTypeAddModel serviceTypeAddModel)
    {
        var response = new ApiResponse();

        var entity = _mapper.Map<ServiceType>(serviceTypeAddModel);

        await _unitOfWork.ServiceTypeRepo.AddAsync(entity);
        await _unitOfWork.SaveChangeAsync();

        return response.SetOk("Created");
    }

    public async Task<ApiResponse> UpdateServiceType(int serviceTypeId, ServiceTypeUpdateModel serviceTypeUpdateModel)
    {
        var response = new ApiResponse();

        var currentEntity = await _unitOfWork.ServiceTypeRepo.GetAsync(x => x.ServiceTypeId == serviceTypeId);

        if (currentEntity is null)
        {
            return response.SetNotFound($"Not Found ServiceType with Id {serviceTypeId}");
        }

        _mapper.Map<ServiceType>(serviceTypeUpdateModel);
        await _unitOfWork.SaveChangeAsync();

        return response.SetOk("Updated");
    }

    public async Task DeleteServiceType(int serviceTypeId)
    {
        await _unitOfWork.ServiceTypeRepo.RemoveByIdAsync(serviceTypeId);
    }

    public async Task<ApiResponse> GetListServiceByServiceTypeId(int serviceTypeId)
    {
        var response = new ApiResponse();
        var services = await _unitOfWork.ServiceRepo.GetAllAsync(x => x.ServiceTypeId == serviceTypeId);
        var servicesResponse = _mapper.Map<List<ServiceResponseModel>>(services);
        return response.SetOk(servicesResponse);
    }
}