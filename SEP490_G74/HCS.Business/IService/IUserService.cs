using HCS.Business.RequestModel.ContactRequestModel;
using HCS.Business.RequestModel.MedicalRecordRequestModel;
using HCS.Business.RequestModel.PatientRequestModel;
using HCS.Business.RequestModel.UserRequestModel;
using HCS.Business.ResponseModel.ApiRessponse;

namespace HCS.Business.IService
{
    public interface IUserService
    {
        public Task<ApiResponse> Login(UserLoginRequestModel user);
        public Task<ApiResponse> AddPatient(PatientAddModel patient);
        public Task<ApiResponse> AddContact(ContactAddModel contact);
        public Task<ApiResponse> GetProfile(int userId);
        Task<ApiResponse> GetPatients(int pageIndex, int pageSize, int userId);
    }
}