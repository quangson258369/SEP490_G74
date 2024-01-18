using AutoMapper;
using HCS.Business.Pagination;
using HCS.Business.RequestModel.MedicalRecordRequestModel;
using HCS.Business.ResponseModel.ApiResponse;
using HCS.Business.ResponseModel.CategoryResponse;
using HCS.Business.ResponseModel.ExaminationResultResponseModel;
using HCS.Business.ResponseModel.MedicalRecordResponseModel;
using HCS.DataAccess.UnitOfWork;
using HCS.Domain.Commons;
using HCS.Domain.Enums;
using HCS.Domain.Models;
using Microsoft.Extensions.Logging.Abstractions;

namespace HCS.Business.Service;
public interface IMedicalRecordService
{
    Task<ApiResponse> AddMedicalRecord(MedicalRecordAddModel medicalRecord);
    Task<ApiResponse> UpdateMedicalRecord(int mrId, MedicalRecordUpdateModel medicalRecordUpdateModel);

    Task<ApiResponse> GetListMrByPatientId(int patientId, int pageIndex, int pageSize, int userId);
    Task<ApiResponse> GetMrById(int id, int userId);
    Task<ApiResponse> UpdateMrStatus(int mrId, bool isPaid, int? userId);
    Task<ApiResponse> NewUpdateMedicalRecord(int userId, int id, NewMedicalRecordUpdateModel newMedicalRecord);
    Task<ApiResponse> GetReCheckUpMedicalRecordByPreviosMedicalRecordId(int prevMrId);
    Task<ApiResponse> GetListMrUnCheckByPatientId(int patientId, int pageIndex, int pageSize, int userId);
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

        if (medicalRecord.PreviousMedicalRecordId != null && medicalRecord.PreviousMedicalRecordId > 0)
        {
            medicalRecordEntity.PreviousMedicalRecordId = medicalRecord.PreviousMedicalRecordId;
        }

        if(medicalRecord.CategoryIds == null || medicalRecord.CategoryIds.Count == 0 || !medicalRecord.CategoryIds.Any(c => c == DefaultMrOption.DefaultCategoryId))
        {
            medicalRecord.CategoryIds ??= new List<int>();
            medicalRecord.CategoryIds.Add(DefaultMrOption.DefaultCategoryId);
        }


        medicalRecordEntity.MedicalRecordCategories = medicalRecord.CategoryIds.Select(
            c => new MedicalRecordCategory()
            {
                CategoryId = c
            }).ToList();

        if (medicalRecord.DoctorIds == null || medicalRecord.DoctorIds.Count == 0 || !medicalRecord.DoctorIds.Any(c => c == DefaultMrOption.DefaultDoctorId))
        {
            medicalRecord.DoctorIds ??= new List<int>();
            medicalRecord.DoctorIds.Add(DefaultMrOption.DefaultDoctorId);
        }

        medicalRecordEntity.MedicalRecordDoctors = medicalRecord.DoctorIds.Select(
            d => new MedicalRecordDoctor()
            {
                DoctorId = d
            }).ToList();

        ServiceMedicalRecord defaultService = new()
        {
            ServiceId = DefaultMrOption.DefaultServiceId,
            MedicalRecord = medicalRecordEntity
        };

        medicalRecordEntity.ServiceMedicalRecords ??= new List<ServiceMedicalRecord>();
        medicalRecordEntity.ServiceMedicalRecords.Add(defaultService);

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

    public async Task<ApiResponse> GetListMrByPatientId(int patientId, int pageIndex, int pageSize, int userId)
    {
        var response = new ApiResponse();

        var listItem = await _unitOfWork.MedicalRecordRepo.GetAllAsync(x => x.PatientId == patientId);

        if (patientId == 0)
        {
            listItem = await _unitOfWork.MedicalRecordRepo.GetAllAsync(null);
            listItem = await GetListMedicalRecordsByRole(userId, listItem);
        }

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

        // order list by date
        listItemResponse = listItemResponse.OrderByDescending(x => x.MedicalRecordDate).ToList();
        var paginationResult = listItemResponse.Paginate(pageIndex, pageSize);
        return response.SetOk(paginationResult);
    }

    public async Task<ApiResponse> GetListMrUnCheckByPatientId(int patientId, int pageIndex, int pageSize, int userId)
    {
        var response = new ApiResponse();

        var listItem = await _unitOfWork.MedicalRecordRepo.GetAllAsync(x => x.PatientId == patientId);

        if (patientId == 0)
        {
            listItem = await _unitOfWork.MedicalRecordRepo.GetAllAsync(null);
            listItem = await GetListMedicalRecordsByRole(userId, listItem);
        }

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
        listItemResponse = listItemResponse.Where(x => x.IsCheckUp == false).ToList();
        // order list by date
        listItemResponse = listItemResponse.OrderByDescending(x => x.MedicalRecordDate).ToList();
        var paginationResult = listItemResponse.Paginate(pageIndex, pageSize);
        return response.SetOk(paginationResult);
    }

    public async Task<ApiResponse> GetMrById(int id, int userId)
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
            PrevMedicalRecordId = mrById.PreviousMedicalRecordId,
            Categories = mrById.MedicalRecordCategories!.Select(
                               c => new CategoryResponseModel()
                               {
                                   CategoryId = c.CategoryId,
                                   CategoryName = c.Category.CategoryName,
                                   IsDeleted = c.Category.IsDeleted

                               }
                                              ).ToList(),
            Doctors = mrById.MedicalRecordDoctors!.Select(
                               d => new DoctorResponseModel()
                               {
                                   DoctorId = d.DoctorId,
                                   DoctorName = d.Doctor.Contact != null ? d.Doctor.Contact.Name : string.Empty,
                                   CategoryId = d.Doctor.CategoryId ?? 0,
                                   IsDeleted = d.Doctor.IsDeleted
                               }
                                                                            ).ToList(),
            IsPaid = mrById.IsPaid,
            IsCheckUp = mrById.IsCheckUp,
        };
        // get serviceTypes
        if (mrById.ServiceMedicalRecords is not null)
        {
            // get selected serviceTypes
            var typeIds = mrById.ServiceMedicalRecords.Select(x => x.Service.ServiceTypeId).Distinct();
            foreach (var typeId in typeIds)
            {
                var typeEntity = await _unitOfWork.ServiceTypeRepo.GetAsync(x => x.ServiceTypeId == typeId);
                if (typeEntity != null)
                {
                    var servicesByTyped = mrById.ServiceMedicalRecords.Where(x => x.Service.ServiceTypeId == typeId).Select(x => x.Service).ToList();
                    mrByIdResponse.ServiceTypes.Add(new ServiceTypeDetailResponseModel()
                    {
                        ServiceTypeId = typeEntity.ServiceTypeId,
                        ServiceTypeName = typeEntity.ServiceTypeName,
                        IsDeleted = typeEntity.IsDeleted,
                        Services = servicesByTyped.Select(x => new ServiceResponseDetailModel()
                        {
                            ServiceId = x.ServiceId,
                            ServiceName = x.ServiceName,
                            Price = x.Price,
                            ServiceTypeId = x.ServiceTypeId,
                            IsDeleted = x.IsDeleted
                        }).ToList()
                    });
                }
            }
        }

        // check if role is doctor => filter mrById list categories, service types, service by doctorId
        if (userId > 0)
        {
            var user = await _unitOfWork.UserRepo.GetAsync(u => u.UserId == userId);
            if (user != null)
            {
                if (user.RoleId == (int)UserRole.Doctor)
                {
                    // filter only doctor call api
                    mrById.MedicalRecordDoctors = mrById.MedicalRecordDoctors?.Where(x => x.DoctorId == userId).ToList();

                    if (mrById.MedicalRecordDoctors != null)
                    {
                        mrByIdResponse.Doctors = mrById.MedicalRecordDoctors.Select(x => new DoctorResponseModel()
                        {
                            DoctorId = x.DoctorId,
                            DoctorName = x.Doctor.Contact != null ? x.Doctor.Contact.Name : string.Empty,
                            CategoryId = x.Doctor.CategoryId ?? 0
                        }).ToList();
                    }

                    // filter category
                    mrById.MedicalRecordCategories = mrById.MedicalRecordCategories?.Where(x => x.CategoryId == user.CategoryId).ToList();

                    if (mrById.MedicalRecordCategories != null)
                    {
                        mrByIdResponse.Categories = mrById.MedicalRecordCategories.Select(x => new CategoryResponseModel()
                        {
                            CategoryId = x.CategoryId,
                            CategoryName = x.Category.CategoryName
                        }).ToList();
                    }

                    List<int> listCateIds = mrById.MedicalRecordCategories?.Select(x => x.CategoryId).ToList() ?? new List<int>();
                    // filter service type
                    if (mrById.ServiceMedicalRecords is not null)
                    {
                        // get list types in mr that have same categoryId with user
                        var listServiceTypeIds = mrById.ServiceMedicalRecords
                            .Where(x => x.Service.ServiceType.CategoryId == user.CategoryId)
                            .Select(x => x.Service.ServiceTypeId)
                            .Distinct()
                            .ToList();
                        // get all type from db then filter by list type ids
                        var types = await _unitOfWork.ServiceTypeRepo.GetAllAsync(null);
                        types = types.Where(t => listServiceTypeIds.Contains(t.ServiceTypeId)).ToList();

                        mrByIdResponse.ServiceTypes = types.Select(x => new ServiceTypeDetailResponseModel()
                        {
                            ServiceTypeId = x.ServiceTypeId,
                            ServiceTypeName = x.ServiceTypeName,
                            Services = new List<ServiceResponseDetailModel>()
                        }).ToList();

                        foreach (var serviceType in mrByIdResponse.ServiceTypes)
                        {
                            serviceType.Services = mrById.ServiceMedicalRecords
                                .Where(x => x.Service.ServiceTypeId == serviceType.ServiceTypeId)
                                .Select(x => new ServiceResponseDetailModel()
                                {
                                    ServiceId = x.Service.ServiceId,
                                    ServiceName = x.Service.ServiceName,
                                    Price = x.Service.Price,
                                    ServiceTypeId = x.Service.ServiceTypeId
                                }).ToList();
                        }
                    }
                }
            }
        }
        return new ApiResponse().SetOk(mrByIdResponse);
    }

    public async Task<ApiResponse> UpdateMrStatus(int mrId, bool isPaid, int? userId)
    {
        switch (isPaid)
        {
            case true:
                {
                    await _unitOfWork.MedicalRecordRepo.UpdateMrStatusToPaid(mrId, userId);
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

        if (existMed is not null)
        {
            var user = await _unitOfWork.UserRepo.GetAsync(u => u.UserId == userId);
            if (user == null) return new ApiResponse().SetNotFound("User Not Found");

            switch (user.RoleId)
            {
                case 4:
                    {
                        if (newMedicalRecord.CategoryIds is not null)
                        {
                            existMed.MedicalRecordCategories?.Clear();
                            existMed.MedicalRecordCategories = newMedicalRecord.CategoryIds.Select(
                                                   c => new MedicalRecordCategory()
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
                            if (existMed.ServiceMedicalRecords is null || existMed.ServiceMedicalRecords.Count < 1)
                            {
                                existMed.ServiceMedicalRecords = new List<ServiceMedicalRecord>();
                                existMed.ServiceMedicalRecords = newMedicalRecord.ServiceIds.Select(
                                                                           s => new ServiceMedicalRecord()
                                                                           {
                                                                               ServiceId = s
                                                                           }).ToList();

                            }
                            else
                            {
                                //Lay danh sach category cua cac service trong medical record
                                Dictionary<int, List<int>> cateServices = new Dictionary<int, List<int>>();

                                foreach (var service in existMed.ServiceMedicalRecords)
                                {
                                    var category = await _unitOfWork.CategoryRepo.GetCategoryByServiceId(service.ServiceId);
                                    if (category != null)
                                    {
                                        if (cateServices.ContainsKey(category.CategoryId))
                                        {
                                            cateServices[category.CategoryId].Add(service.ServiceId);
                                        }
                                        else
                                        {
                                            cateServices.Add(category.CategoryId, new List<int>() { service.ServiceId });
                                        }
                                    }
                                }
                                // lay danh sach category moi va service moi
                                Dictionary<int, List<int>> newCateServices = new Dictionary<int, List<int>>();

                                foreach (var serviceId in newMedicalRecord.ServiceIds)
                                {
                                    var category = await _unitOfWork.CategoryRepo.GetCategoryByServiceId(serviceId);
                                    if (category != null)
                                    {
                                        if (newCateServices.ContainsKey(category.CategoryId))
                                        {
                                            newCateServices[category.CategoryId].Add(serviceId);
                                        }
                                        else
                                        {
                                            newCateServices.Add(category.CategoryId, new List<int>() { serviceId });
                                        }
                                    }
                                }
                                // remove all service has same category with cateServices
                                foreach (var oldService in existMed.ServiceMedicalRecords)
                                {
                                    var cate = await _unitOfWork.CategoryRepo.GetCategoryByServiceId(oldService.ServiceId);
                                    if (cate != null)
                                    {
                                        if (newCateServices.ContainsKey(cate.CategoryId))
                                        {
                                            existMed.ServiceMedicalRecords.Remove(oldService);
                                        }
                                    }
                                }

                                // parse new services of same category doctor to current services
                                foreach (var newCate in newCateServices)
                                {
                                    // if current cateServices contains new category, update new serviceIds for selected categoryId
                                    if (cateServices.ContainsKey(newCate.Key))
                                    {
                                        cateServices[newCate.Key] = newCate.Value;
                                    }
                                    // if current cateServices not contains new category, add new category and serviceIds
                                    else
                                    {
                                        var newServiceIds = newCate.Value;
                                        foreach(var newServiceId in newServiceIds)
                                        {
                                            if (!existMed.ServiceMedicalRecords.Select(x => x.ServiceId).Contains(newServiceId))
                                            {
                                                existMed.ServiceMedicalRecords.Add(new ServiceMedicalRecord()
                                                {
                                                    ServiceId = newServiceId
                                                });
                                            }
                                        }
                                    }
                                }

                                if (user.CategoryId is not null)
                                {
                                    if (cateServices.ContainsKey((int)user.CategoryId))
                                    {
                                        var newServiceIds = cateServices[(int)user.CategoryId];
                                        foreach (var serviceId in newServiceIds)
                                        {
                                            if (!existMed.ServiceMedicalRecords.Select(x => x.ServiceId).Contains(serviceId))
                                            {
                                                existMed.ServiceMedicalRecords.Add(new ServiceMedicalRecord()
                                                {
                                                    ServiceId = serviceId
                                                });
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    }
                case 1:
                    {
                        if (newMedicalRecord.CategoryIds is not null)
                        {
                            existMed.MedicalRecordCategories?.Clear();
                            existMed.MedicalRecordCategories = newMedicalRecord.CategoryIds.Select(
                                                   c => new MedicalRecordCategory()
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

    public async Task<ApiResponse> GetReCheckUpMedicalRecordByPreviosMedicalRecordId(int prevMrId)
    {
        var response = new ApiResponse();
        var prevMr = await _unitOfWork.MedicalRecordRepo.GetAsync(x => x.PreviousMedicalRecordId == prevMrId);
        if (prevMr is null)
        {
            return new ApiResponse().SetNotFound("Not Found");
        }
        response.SetOk(prevMr.MedicalRecordId);
        return response;
    }

    public async Task<List<MedicalRecord>> GetListMedicalRecordsByRole(int userId, List<MedicalRecord> medicalRecords)
    {
        var user = await _unitOfWork.UserRepo.GetAsync(u => u.UserId == userId);
        if (user == null) return medicalRecords;
        if (user.RoleId != (int)UserRole.Doctor) return medicalRecords;

        var filteredListMr = new List<MedicalRecord>();

        foreach (var mr in medicalRecords)
        {
            var mrDetail = await _unitOfWork.MedicalRecordRepo.GetMrById(mr.MedicalRecordId);

            if (mrDetail != null && mrDetail.MedicalRecordDoctors != null)
            {
                foreach (var mrDoctor in mrDetail.MedicalRecordDoctors)
                {
                    if (mrDoctor.DoctorId == user.UserId)
                    {
                        filteredListMr.Add(mr);
                    }
                }
            }
        }
        return filteredListMr;
    }
}