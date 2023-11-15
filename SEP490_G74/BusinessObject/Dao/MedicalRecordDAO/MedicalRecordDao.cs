using API.Common;
using API.Common.Entity;
using HcsBE.Dao.Login;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.Dao.MedicalRecordDAO
{
    public class MedicalRecordDao
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public List<MedicalRecord> MedicalRecordList()
        {
            var query = context.MedicalRecords.ToList();
            return query;
        }

        public MedicalRecord GetMedicalRecord(int id)
        {
            var query = context.MedicalRecords.Include(x => x.Doctor)
                .Include(y => y.Patient)
                .Include(x => x.Services)
                .Include(x => x.ExaminationResultIds)
                .Include(x => x.Prescriptions)
                .Select( x => new MedicalRecord
                {
                    Doctor = x.Doctor,
                    DoctorId = x.DoctorId,
                    ExamCode = x.ExamCode,
                    ExaminationResultIds = x.ExaminationResultIds,
                    ExamReason = x.ExamReason,
                    MedicalRecordDate = x.MedicalRecordDate,
                    MedicalRecordId = x.MedicalRecordId,
                    Patient = x.Patient,
                    PatientId = x.PatientId,
                    Prescriptions = x.Prescriptions,
                    Services = x.Services
                }).SingleOrDefault(x => x.MedicalRecordId == id);
            return query;
        }


    }
}
