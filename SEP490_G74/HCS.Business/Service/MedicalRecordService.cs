using AutoMapper;
using HCS.Business.Pagination;
using HCS.Business.RequestModel.MedicalRecordRequestModel;
using HCS.Business.ResponseModel.ApiResponse;
using HCS.Business.ResponseModel.CategoryResponse;
using HCS.Business.ResponseModel.MedicalRecordResponseModel;
using HCS.DataAccess.UnitOfWork;
using HCS.Domain.Enums;
using HCS.Domain.Models;
using Microsoft.Extensions.Logging.Abstractions;

namespace HCS.Business.Service;
public interface IMedicalRecordService
{
    Task<ApiResponse> AddMedicalRecord(MedicalRecordAddModel medicalRecord);
    Task<ApiResponse> UpdateMedicalRecord(int mrId, MedicalRecordUpdateModel medicalRecordUpdateModel);

    Task<ApiResponse> GetListMrByPatientId(int patientId, int pageIndex, int pageSize);
    Task<ApiResponse> GetMrById(int id);
    Task<ApiResponse> UpdateMrStatus(int mrId, bool isPaid);
    Task<ApiResponse> NewUpdateMedicalRecord(int userId, int id, NewMedicalRecordUpdateModel newMedicalRecord);
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
        var medicalRecordEntity = new MedicalRecord()
        {
            MedicalRecordDate = DateTime.Now,
            ExamReason = medicalRecord.ExamReason,
            PatientId = medicalRecord.PatientId,
            IsCheckUp = false,
            IsPaid = false
        };
        medicalRecordEntity.MedicalRecordCategories = medicalRecord.CategoryIds.Select(
            c => new MedicalRecordCateogry()
            {
                CategoryId = c
            }).ToList();
                       
        medicalRecordEntity.MedicalRecordDoctors = medicalRecord.DoctorIds.Select(  
            d => new MedicalRecordDoctor()
            {
                DoctorId = d
            }).ToList();

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
        //currentMr.ExamCode = medicalRecordUpdateModel.ExamCode;

        await _unitOfWork.SaveChangeAsync();

        return response.SetOk("Updated");
    }

    public async Task<ApiResponse> GetListMrByPatientId(int patientId, int pageIndex, int pageSize)
    {
        var response = new ApiResponse();

        var listItem = await _unitOfWork.MedicalRecordRepo.GetAllAsync(x => x.PatientId == patientId);

        var listItemResponse = new List<MrResponseByPatientId>();

        foreach (var item in listItem)
        {
            var contact = await _unitOfWork.ContactRepo.GetAsync(x => x.Patient!.PatientId == item.PatientId);

            //var category = await _unitOfWork.CategoryRepo.GetAsync(x => x.CategoryId == item.CategoryId);
            var result = new MrResponseByPatientId()
            {
                MedicalRecordId = item.MedicalRecordId,
                MedicalRecordDate = item.MedicalRecordDate,
                Name = contact.Name,
                //CategoryName = category.CategoryName,
                IsCheckUp = item.IsCheckUp,
                IsPaid = item.IsPaid,
                PatientId = item.PatientId
            };

            listItemResponse.Add(result);
        }

        listItemResponse.Paginate(pageIndex, pageSize);
        return response.SetOk(listItemResponse.OrderByDescending(x => x.MedicalRecordDate));
    }

    public async Task<ApiResponse> GetMrById(int id)
    {
        var mrById = await _unitOfWork.MedicalRecordRepo.GetMrById(id);
        if (mrById is null)
        {
            return new ApiResponse().SetNotFound($"Not Found with Id {id}");
        }
        var mrByIdResponse = new MedicalRecordDetailResponseModel()
        {
            MedicalRecordId = mrById.MedicalRecordId,
            MedicalRecordDate = mrById.MedicalRecordDate,
            ExamReason = mrById.ExamReason,
            ExamCode = mrById.ExamReason,
            PatientId = mrById.PatientId,
            Categories = mrById.MedicalRecordCategories!.Select(
                               c => new CategoryResponseModel()
                               {
                    CategoryId = c.CategoryId,
                    CategoryName = c.Category.CategoryName
                }
                                              ).ToList(),
            Doctors = mrById.MedicalRecordDoctors!.Select(
                               d => new DoctorResponseModel()
                               {
                    DoctorId = d.DoctorId,
                    DoctorName = d.Doctor.Contact != null ? d.Doctor.Contact.Name : string.Empty,
                    CategoryId = d.Doctor.CategoryId ?? 0
                }
                                                                            ).ToList(),
            IsPaid = mrById.IsPaid,
            IsCheckUp = mrById.IsCheckUp,
        };
        // get serviceTypes
        if(mrById.ServiceMedicalRecords is not null)
        {
            // get selected serviceTypes
            var typeIds = mrById.ServiceMedicalRecords.Select(x => x.Service.ServiceTypeId).Distinct();
            foreach(var typeId in typeIds)
            {
                var typeEntity = await _unitOfWork.ServiceTypeRepo.GetAsync(x => x.ServiceTypeId == typeId);
                if (typeEntity != null)
                {
                    var servicesByTyped = mrById.ServiceMedicalRecords.Where(x => x.Service.ServiceTypeId == typeId).Select(x => x.Service).ToList();
                    mrByIdResponse.ServiceTypes.Add(new ServiceTypeDetailResponseModel()
                    {
                        ServiceTypeId = typeEntity.ServiceTypeId,
                        ServiceTypeName = typeEntity.ServiceTypeName,
                        Services = servicesByTyped.Select(x => new ServiceResponseDetailModel()
                        {
                            ServiceId = x.ServiceId,
                            ServiceName = x.ServiceName,
                            Price = x.Price,
                            ServiceTypeId = x.ServiceTypeId
                        }).ToList()
                    });
                }
            }
        }
        return new ApiResponse().SetOk(mrByIdResponse);
    }

    public async Task<ApiResponse> UpdateMrStatus(int mrId, bool isPaid)
    {
        switch (isPaid)
        {
            case true:
                {
                    await _unitOfWork.MedicalRecordRepo.UpdateMrStatusToPaid(mrId);
                    break;
                }
            case false:
                {
                    await _unitOfWork.MedicalRecordRepo.UpdateMrStatusToCheckUp(mrId);
                    break;
                }
        }
        await _unitOfWork.SaveChangeAsync();
        return new ApiResponse().SetOk("Updated");
    }

    public async Task<ApiResponse> NewUpdateMedicalRecord(int userId, int id, NewMedicalRecordUpdateModel newMedicalRecord)
    {
        var existMed = await _unitOfWork.MedicalRecordRepo.GetMrById(id);

        if(existMed is not null)
        {
            var user = await _unitOfWork.UserRepo.GetAsync(u => u.UserId == userId);
            if(user == null) return new ApiResponse().SetNotFound("User Not Found");

            switch (user.RoleId)
            {
                case 4:
                    {
                        if (newMedicalRecord.CategoryIds is not null)
                        {
                            existMed.MedicalRecordCategories?.Clear();
                            existMed.MedicalRecordCategories = newMedicalRecord.CategoryIds.Select(
                                                   c => new MedicalRecordCateogry()
                                                   {
                                                       CategoryId = c
                                                   }).ToList();
                        }

                        if (newMedicalRecord.DoctorIds is not null)
                        {
                            existMed.MedicalRecordDoctors?.Clear();
                            existMed.MedicalRecordDoctors = newMedicalRecord.DoctorIds.Select(
                                                                      d => new MedicalRecordDoctor()
                                                                      {
                                                                          DoctorId = d
                                                                      }).ToList();
                        }
                        break;
                    }
                case 2:
                    {
                        if (newMedicalRecord.ServiceIds is not null)
                        {
                            existMed.ServiceMedicalRecords?.Clear();
                            existMed.ServiceMedicalRecords = newMedicalRecord.ServiceIds.Select(
                                                                                         s => new ServiceMedicalRecord()
                                                                                         {
                                                                                             ServiceId = s
                                                                                         }).ToList();
                        }
                        break;
                    }
                case 1:
                    {
                        if (newMedicalRecord.CategoryIds is not null)
                        {
                            existMed.MedicalRecordCategories?.Clear();
                            existMed.MedicalRecordCategories = newMedicalRecord.CategoryIds.Select(
                                                   c => new MedicalRecordCateogry()
                                                   {
                                                       CategoryId = c
                                                   }).ToList();
                        }

                        if (newMedicalRecord.DoctorIds is not null)
                        {
                            existMed.MedicalRecordDoctors?.Clear();
                            existMed.MedicalRecordDoctors = newMedicalRecord.DoctorIds.Select(
                                                                      d => new MedicalRecordDoctor()
                                                                      {
                                                                          DoctorId = d
                                                                      }).ToList();
                        }

                        if (newMedicalRecord.ServiceIds is not null)
                        {
                            existMed.ServiceMedicalRecords?.Clear();
                            existMed.ServiceMedicalRecords = newMedicalRecord.ServiceIds.Select(
                                                                                         s => new ServiceMedicalRecord()
                                                                                         {
                                                                                             ServiceId = s
                                                                                         }).ToList();
                        }
                        break;
                    }
                default:
                    {
                        return new ApiResponse().SetBadRequest("Not Found");
                    }
            }

            await _unitOfWork.SaveChangeAsync();

            return new ApiResponse().SetOk("Updated");
        }
        return new ApiResponse().SetBadRequest("Not Found");
    }
}