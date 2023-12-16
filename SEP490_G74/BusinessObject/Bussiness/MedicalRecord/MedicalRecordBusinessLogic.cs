using API.Common;
using AutoMapper;
using HcsBE.Bussiness.Login;
using HcsBE.Dao.MedicalRecordDAO;
using HcsBE.Mapper;
using HcsBE.DTO;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.Bussiness.MedicalRecord
{
    public class MedicalRecordBusinessLogic
    {
        private MedicalRecordDao dao = new MedicalRecordDao();
        private IMapper _mapper;
        public MedicalRecordBusinessLogic(IMapper mapper)
        {
            _mapper = mapper;
        }

        public List<MedicalRecordOutputDto> SearchMedicalRecord(string str, int page)
        {
            List<DataAccess.Entity.MedicalRecord> list = dao.searchMR(str, page);
            var output = _mapper.Map<List<MedicalRecordOutputDto>>(list);
            if (list == null)
            {
                return new List<MedicalRecordOutputDto>()
                {
                    new MedicalRecordOutputDto()
                    {
                        ExceptionMessage = ConstantHcs.NotFound,
                        ResultCd = ConstantHcs.BussinessError
                    }
                };
            }
            return output;
        }

        public List<MedicalRecordOutputDto> GetListMedicalRecord()
        {
            List<DataAccess.Entity.MedicalRecord> list = dao.MedicalRecordList();
            var output = _mapper.Map<List<MedicalRecordOutputDto>>(list);
            if(list == null)
            {
                return new List<MedicalRecordOutputDto>()
                {
                    new MedicalRecordOutputDto()
                    {
                        ExceptionMessage = ConstantHcs.NotFound,
                        ResultCd = ConstantHcs.BussinessError
                    }
                };
            }
            return output;
        }
        public List<MedicalRecordOutputDto> GetListMedicalRecordPaging(int page)
        {
            List<DataAccess.Entity.MedicalRecord> list = dao.MedicalRecordListPaging(page);
            var output = _mapper.Map<List<MedicalRecordOutputDto>>(list);
            if(list == null)
            {
                return new List<MedicalRecordOutputDto>()
                {
                    new MedicalRecordOutputDto()
                    {
                        ExceptionMessage = ConstantHcs.NotFound,
                        ResultCd = ConstantHcs.BussinessError
                    }
                };
            }
            return output;
        }

        public int GetCountOfListMR()
        {
            return dao.GetCountOfListMR();
        }

        public MedicalRecordOutputDto GetMedicalRecord(int id) {
        
            var mr = dao.GetMedicalRecord(id);
            var output = _mapper.Map<MedicalRecordOutputDto>(mr);

            if (mr == null)
            {
                return new MedicalRecordOutputDto()
                {
                    ExceptionMessage = ConstantHcs.NotFound,
                    ResultCd = ConstantHcs.BussinessError
                };
            }
            return output;
        }

        public bool Update( MedicalRecordModify mdto)
        {
            var mr = _mapper.Map<DataAccess.Entity.MedicalRecord>(mdto);
            var status = dao.UpdateMedicalRecord(mr);
            return status;
        }

        public bool AddMR(MedicalRecordModify mdto) 
        {
            var mr = _mapper.Map<DataAccess.Entity.MedicalRecord>(mdto);
            var status = dao.AddMedicalRecord(mr);
            return status;
        }
        
        public string DeleteMR(int id) 
        {
            var mdto = dao.GetMedicalRecord(id);
            if(mdto != null)
            {
                var status = dao.DeleteMedicalRecord(id);
                if(status == "-1")
                {
                    return "-1";
                }
                else
                {
                    return "1";
                }
            }
            return "0";
        }

        public List<DoctorMRDTO> GetDoctorByServiceType(int type)
        {
            var list = dao.GetDoctorByServiceType(type);
            return _mapper.Map<List<DoctorMRDTO>>(list);
        }

        public List<ServiceMRDTO> GetListServiceUses(int id)
        {
            var list = dao.GetServiceUses(id);
            return _mapper.Map<List<ServiceMRDTO>>(list);
        }
    }
}
