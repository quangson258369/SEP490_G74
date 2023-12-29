using HCS.Business.RequestModel.MedicalRecordRequestModel;
using HCS.Business.ResponseModel.ApiRessponse;

namespace HCS.Business.IService;

public interface IMedicalRecordService
{
    Task<ApiResponse> AddMedicalRecord(MedicalRecordAddModel medicalRecord);
}