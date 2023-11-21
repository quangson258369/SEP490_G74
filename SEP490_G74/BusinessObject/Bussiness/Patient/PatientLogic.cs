using API.Common;
using AutoMapper;
using HcsBE.Dao.MedicalRecordDAO;
using HcsBE.Dao.PatientDao;
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

        public List<PatientDTO> ListPatient()
        {
            // khoi tao mapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PatientMapper());
            });
            var mapper = config.CreateMapper();
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
    }
}
