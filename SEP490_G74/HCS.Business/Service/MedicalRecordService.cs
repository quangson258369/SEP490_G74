using AutoMapper;
using HCS.Business.IService;
using HCS.Business.RequestModel.MedicalRecordRequestModel;
using HCS.Business.ResponseModel.ApiRessponse;
using HCS.DataAccess.UnitOfWork;
using HCS.Domain.Models;

namespace HCS.Business.Service;

public class MedicalRecordService : IMedicalRecordService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MedicalRecordService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ApiResponse> AddMedicalRecord(MedicalRecordAddModel medicalRecord)
    {

        var medicalRecordEntity = _mapper.Map<MedicalRecord>(medicalRecord);

        await _unitOfWork.MedicalRecordRepo.AddAsync(medicalRecordEntity);
        await _unitOfWork.SaveChangeAsync();
        
        var response = new ApiResponse();
        response.SetOk("Created");
        
        return response;
    }
}