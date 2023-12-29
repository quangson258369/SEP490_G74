using AutoMapper;
using HCS.DataAccess.UnitOfWork;
using HCS.Domain.Models;
using HCS.Business.IService;
using HCS.Business.RequestModel.ContactRequestModel;
using HCS.Business.RequestModel.MedicalRecordRequestModel;
using HCS.Business.RequestModel.PatientRequestModel;
using HCS.Business.RequestModel.UserRequestModel;
using HCS.Business.ResponseModel.ApiRessponse;
using HCS.Business.ResponseModel.PatientResponseModel;
using HCS.Business.Util.JWT;
using HCS.Business.Util.MD5PasswordGenerator;
using HCS.Business.Pagination;

namespace HCS.Business.Service
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse> Login(UserLoginRequestModel user)
        {
            var pwd = PasswordGenerator.GetMD5Hash(user.Password);
            var account = await _unitOfWork.UserRepo
                .GetAsync(u => u.Email.Equals(user.Email) && u.Password.Equals(pwd));

            var loginResponse = new ApiResponse();
            if (account != null)
            {
                var jwtUser = await _unitOfWork.UserRepo.GetProfile(account.Email);

                if (jwtUser != null)
                {
                    var token = JwtTokenHelper.CreateToken(jwtUser);
                    loginResponse.SetOk(token);
                }
            }
            else
            {
                loginResponse.SetNotFound(message: $"Email Not Found: {user.Email}");
            }

            return loginResponse;
        }

        public async Task<ApiResponse> AddPatient(PatientAddModel patient)
        {
            var patientEntity = _mapper.Map<Patient>(patient);
            try
            {
                await _unitOfWork.PatientRepo.AddAsync(patientEntity);
                await _unitOfWork.SaveChangeAsync();
                var response = new ApiResponse();
                response.SetOk("Created");
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse> AddContact(ContactAddModel contact)
        {
            var contactEntity = _mapper.Map<Contact>(contact);
            try
            {
                await _unitOfWork.ContactRepo.AddAsync(contactEntity);
                var response = new ApiResponse();
                response.SetOk("Created");
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public async Task<ApiResponse> GetProfile(int userId)
        {
            ApiResponse res = new();
            var account = await _unitOfWork.UserRepo.GetAsync(u => u.UserId == userId);

            if (account == null)
            {
                res.SetNotFound("User Not Found With ID: " + userId);
                return res;
            }

            var profile = await _unitOfWork.UserRepo.GetProfile(account.Email);

            if (profile == null)
            {
                res.SetNotFound("User Profile Not Found With Email: " + account.Email);
                return res;
            }

            res.SetOk(profile);
            return res;
        }

        public async Task<ApiResponse> GetPatients(int pageIndex, int pageSize, int userId)
        {
            ApiResponse res = new();

            var patients = await _unitOfWork.PatientRepo.GetPatients(userId);

            var listOfPatients = _mapper.Map<List<PatientResponseModel>>(patients);

            var patientsResponse = patients.Paginate(pageIndex, pageSize);

            return res.SetOk(patientsResponse);
        }


        //public async Task<ApiResponse> GetPatients(int userId)
        //{
        //    ApiResponse response = new();
        //    var account = await _unitOfWork.UserRepo.GetAsync(u => u.UserId == userId);
        //    switch (account.RoleId)
        //    {
        //        case (int)UserRole.Admin:
        //            {
        //                break;
        //            }
        //    }
        //}
    }
}