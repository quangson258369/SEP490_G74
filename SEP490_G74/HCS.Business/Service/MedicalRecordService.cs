using AutoMapper;
using HCS.Business.Pagination;
using HCS.Business.RequestModel.MedicalRecordRequestModel;
using HCS.Business.ResponseModel.ApiResponse;
using HCS.Business.ResponseModel.MedicalRecordResponseModel;
using HCS.DataAccess.UnitOfWork;
using HCS.Domain.Models;

namespace HCS.Business.Service;
public interface IMedicalRecordService
{
    Task<ApiResponse> AddMedicalRecord(MedicalRecordAddModel medicalRecord);
    Task<ApiResponse> UpdateMedicalRecord(int mrId, MedicalRecordUpdateModel medicalRecordUpdateModel);

    Task<ApiResponse> GetListMrByPatientId(int patientId, int  pageIndex, int pageSize);
}
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
        var response = new ApiResponse();
        var medicalRecordEntity = _mapper.Map<MedicalRecord>(medicalRecord);
        
        await _unitOfWork.MedicalRecordRepo.AddAsync(medicalRecordEntity);
        await _unitOfWork.SaveChangeAsync();
        
        response = new ApiResponse();
        response.SetOk("Created");
        
        return response;
    }

    public async Task<ApiResponse> UpdateMedicalRecord(int mrId, MedicalRecordUpdateModel medicalRecordUpdateModel)
    {
        var response = new ApiResponse();

        var currentMr = await _unitOfWork.MedicalRecordRepo.GetAsync(x => x.MedicalRecordId == mrId);

        if (currentMr is null)
        {
            return response.SetNotFound($"Not Found with Id {mrId}");
        }

        currentMr.MedicalRecordDate = medicalRecordUpdateModel.MedicalRecordDate;
        currentMr.ExamReason = medicalRecordUpdateModel.ExamReason;
        currentMr.ExamCode = medicalRecordUpdateModel.ExamCode;

        await _unitOfWork.SaveChangeAsync();

        return response.SetOk("Updated");
    }

    public async Task<ApiResponse> GetListMrByPatientId(int patientId, int  pageIndex, int pageSize)
    {
        var response = new ApiResponse();
    
        var listItem = await _unitOfWork.MedicalRecordRepo.GetAllAsync(x => x.PatientId == patientId);
        
        var listItemResponse = new List<MrResponseByPatientId>();

        foreach (var item in listItem)
        {
            var contact = await _unitOfWork.ContactRepo.GetAsync(x => x.PatientId == item.PatientId);

            var category = await _unitOfWork.CategoryRepo.GetAsync(x => x.CategoryId == item.CategoryId);
            var result = new MrResponseByPatientId()
            {
                MedicalRecordId = item.MedicalRecordId,
                MedicalRecordDate = item.MedicalRecordDate,
                Name = contact.Name,
                CategoryName = category.CategoryName,
                PatientId = item.PatientId
            };
            
            listItemResponse.Add(result);
        }

        listItemResponse.Paginate(pageIndex, pageSize);
        return response.SetOk(listItemResponse);
    }
}