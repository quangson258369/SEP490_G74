using DataAccess.Entity;
using AutoMapper;
using HcsBE.Dao.MedicalRecordDAO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.Bussiness.MedicalRecord
{
    public class MedicalRCMapper:Profile
    {
        public MedicalRCMapper() {
            CreateMap<MedicalRecordDaoOutputDto, DataAccess.Entity.MedicalRecord>();
            CreateMap<DataAccess.Entity.MedicalRecord, MedicalRecordDaoOutputDto>();
        
        }
    }
}
