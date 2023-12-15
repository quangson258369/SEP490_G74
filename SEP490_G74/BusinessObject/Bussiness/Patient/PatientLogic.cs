using API.Common;
using AutoMapper;
using HcsBE.Dao.PatientDao;
using HcsBE.Mapper;
using HcsBE.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entity;

namespace HcsBE.Bussiness.Patient
{
    public class PatientLogic
    {
        private PatientDAO dao = new PatientDAO();
        private IMapper mapper;
        public PatientLogic( IMapper mapper)
        {
            this.mapper = mapper;
        }

        public List<PatientDTO> ListPatient()
        {
            List<DataAccess.Entity.Patient> list = dao.ListPatients();
            var output =  mapper.Map<List<PatientDTO>>(list);
            if (list == null)
            {
                return new List<PatientDTO>()
                {
                    new PatientDTO()
                    {
                        ExceptionMessage = ConstantHcs.NotFound,
                        ResultCd = ConstantHcs.BussinessError
                    }
                };
            }
            return output;
        }

        public List<MedicalRecordOutputDto> ListMedicalRecordByPatient(int pid)
        {
            List<DataAccess.Entity.MedicalRecord> list = dao.ListMRByPatient(pid);
            var listConvert = mapper.Map<List<MedicalRecordOutputDto>> (list);
            if(listConvert == null)
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
            return listConvert;
        }

        public List<PatientDTO> ListPatientPaging(int page)
        {
            List<DataAccess.Entity.Patient> list = dao.ListPatientPaging(page);
            var output =  mapper.Map<List<PatientDTO>>(list);
            if (list == null)
            {
                return new List<PatientDTO>()
                {
                    new PatientDTO()
                    {
                        ExceptionMessage = ConstantHcs.NotFound,
                        ResultCd = ConstantHcs.BussinessError
                    }
                };
            }
            return output;
        }

        public int GetCountOfListPatient()
        {
            return dao.GetCountOfListPatient();
        }

        public PatientDTO GetPatientGetId(int id)
        {
            DataAccess.Entity.Patient patient = dao.GetPatientById(id);
            if (patient == null)
            {
                return new PatientDTO()
                {
                    ExceptionMessage = ConstantHcs.NotFound,
                    ResultCd = ConstantHcs.BussinessError
                };
            }
            return mapper.Map<PatientDTO>(patient);
        }

        public bool Update(PatientModify entity)
        {
            var pUpdate = mapper.Map<DataAccess.Entity.Patient>(entity);
            var status = dao.UpdatePatient(pUpdate);
            return status;
        }

        public bool UpdateContactForPatient(ContactPatientDTO c)
        {
            var contact = mapper.Map<Contact>(c);
            var status = dao.UpdateContactForThisP(contact);
            return status;
        }
        public bool AddContactForPatient(ContactPatientDTO c)
        {
            var contact = mapper.Map<Contact>(c);
            var status = dao.addContactForPatient(contact);
            return status;
        }

        public bool Add(PatientModify entity)
        {
            var status = dao.AddPatient(mapper.Map<DataAccess.Entity.Patient>(entity));
            return status;
        }
        
        public string Delete(int id)
        {
            var status = dao.DeletePatient(id);
            return status;
        }

        public List<PatientDTO> SearchPatient(string name) 
        {
            var result = dao.SearchPatient(name);
            if (result == null) result = new List<DataAccess.Entity.Patient>();
            var output = mapper.Map<List<PatientDTO>>(result);
            return output;
        }
    }
}
