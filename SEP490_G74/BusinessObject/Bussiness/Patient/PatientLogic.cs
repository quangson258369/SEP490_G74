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
            var p = mapper.Map<PatientDTO>(entity);
            var status = dao.UpdatePatient(mapper.Map<DataAccess.Entity.Patient>(p));
            return status;
        }
        public bool Add(PatientModify entity)
        {
            var p = mapper.Map<PatientDTO>(entity);
            var status = dao.AddPatient(mapper.Map<DataAccess.Entity.Patient>(p));
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
