using AutoMapper;
using DataAccess.Entity;
using HcsBE.Dao.PatientDao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.Bussiness.Patient
{
    public class PatientMapper:Profile
    {
        public PatientMapper() {
            CreateMap<PatientDTO, DataAccess.Entity.Patient>();
            CreateMap<DataAccess.Entity.Patient, PatientDTO>();

        }
    }
}
