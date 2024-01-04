using AutoMapper;
using HCS.DataAccess.UnitOfWork;
using HCS.Domain.Models;
using HCS.Business.RequestModel.UserRequestModel;
using HCS.Business.ResponseModel.ApiResponse;
using HCS.Business.ResponseModel.PatientResponseModel;
using HCS.Business.Util.JWT;
using HCS.Business.Util.MD5PasswordGenerator;
using HCS.Business.Pagination;
using HCS.Business.RequestModel.ContactRequestModel;
using HCS.Business.RequestModel.PatientContactRequestModel;
using HCS.Business.RequestModel.PatientRequestModel;
using HCS.Business.ResponseModel.UserResponseModel;
using static HCS.Business.Util.MD5PasswordGenerator.PasswordGenerator;


namespace HCS.Business.Service
{
    public interface IUserService
    {
        public Task<ApiResponse> Login(UserLoginRequestModel user);
        public Task<ApiResponse> GetProfile(int userId);

        Task<ApiResponse> RegisterUser(UserRegisterModel registerUser);

        Task<ApiResponse> GetPatients(int pageIndex, int pageSize, int userId);

        Task<ApiResponse> AddPatientContact(PatientContactRequestModel patientContactRequestModel);

        Task<ApiResponse> GetListDoctorByCategoryId(int categoryId);
    }

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

        public async Task<ApiResponse> RegisterUser(UserRegisterModel registerUser)
        {
            var response = new ApiResponse();

            var isMatchPassword = IsMatchPassword(user: registerUser);

            if (isMatchPassword is false)
            {
                return response.SetBadRequest(message: "Confirm Password does not matched");
            }

            var newUser = new User()
            {
                Password = GetMD5Hash(registerUser.Password),
                Email = registerUser.Email,
                Status = true,
                RoleId = registerUser.RoleId,
                CategoryId = registerUser.CategoryId
            };

            await _unitOfWork.UserRepo.AddAsync(newUser);
            await _unitOfWork.SaveChangeAsync();
            return response.SetOk("Created");
        }

        private static bool IsMatchPassword(UserRegisterModel user)
        {
            return user.Password is not null && user.Password.Equals(user.ConfirmPassword);
        }

        public async Task<ApiResponse> GetPatients(int pageIndex, int pageSize, int userId)
        {
            ApiResponse res = new();

            var patients = await _unitOfWork.PatientRepo.GetPatients(userId);

            var listOfPatients = _mapper.Map<List<PatientResponseModel>>(patients);

            var patientsResponse = listOfPatients.Paginate(pageIndex, pageSize);

            return res.SetOk(patientsResponse);
        }

        public async Task<ApiResponse> AddPatientContact(PatientContactRequestModel patientContactRequestModel)
        {
            var response = new ApiResponse();

            var patientRequest = new Patient()
            {
                ServiceDetailName = patientContactRequestModel.ServiceDetailName,
                Height = patientContactRequestModel.Height,
                Weight = patientContactRequestModel.Weight,
                BloodGroup = patientContactRequestModel.BloodGroup,
                BloodPressure = patientContactRequestModel.BloodPressure,
                Allergieshistory = patientContactRequestModel.AllergiesHistory
            };

            await _unitOfWork.PatientRepo.AddAsync(patientRequest);
            await _unitOfWork.SaveChangeAsync();

            var currentPatient =
                await _unitOfWork.PatientRepo.GetAsync(
                    x => x.PatientId == patientRequest.PatientId);

            if (currentPatient is null)
            {
                return response.SetNotFound($"Not Found Patient with Id {patientRequest.PatientId}");
            }

            var contactRequest = new Contact()
            {
                Name = patientContactRequestModel.Name,
                Gender = patientContactRequestModel.Gender,
                Phone = patientContactRequestModel.Phone,
                Dob = patientContactRequestModel.Dob,
                Address = patientContactRequestModel.Address,
                Img = patientContactRequestModel.Img,
                PatientId = currentPatient.PatientId,
                UserId = patientContactRequestModel.UserId
            };

            await _unitOfWork.ContactRepo.AddAsync(contactRequest);
            await _unitOfWork.SaveChangeAsync();

            return response.SetOk("Created");
        }

        public async Task<ApiResponse> GetListDoctorByCategoryId(int categoryId)
        {
            var response = new ApiResponse();

            var listExistUsers = await _unitOfWork.UserRepo.GetAllAsync(x => x.CategoryId == categoryId);

            var listDoctorResponse = new List<UserResponseByCategoryModel>();
            foreach (var item in listExistUsers)
            {
                var existRole = await _unitOfWork.RoleRepo.GetAsync(x => x.RoleId == item.RoleId);
                var roleName = string.Empty;

                if (existRole.RoleName != "Doctor") continue;
                {
                    roleName = existRole.RoleName;

                    var existCategory = await _unitOfWork.CategoryRepo.GetAsync(x => x.CategoryId == item.CategoryId);

                    var result = new UserResponseByCategoryModel()
                    {
                        CategoryName = existCategory.CategoryName,
                        RoleName = roleName,
                        CategoryId = item.CategoryId,
                        RoleId = item.RoleId,
                        UserId = item.UserId,
                    };

                    var profile = await _unitOfWork.UserRepo.GetProfile(item.Email);

                    if(profile is not null)
                    {
                        result.UserName = profile.UserName;
                    }
                    else 
                    {
                        result.UserName = "Nguyễn Văn Sơn";
                    }


                    listDoctorResponse.Add(result);
                }
            }

            return response.SetOk(listDoctorResponse);
        }
    }
}