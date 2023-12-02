using DataAccess.Entity;
using HcsBE.DTO;
using Microsoft.EntityFrameworkCore;
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
        private HcsContext context = new HcsContext();

        public List<MedicalRecord> MedicalRecordList()
        {
            var query = context.MedicalRecords.ToList();
            return query;
        }

        public MedicalRecord GetMedicalRecord(int id)
        {
            var query = context.MedicalRecords
                .Select(x => new MedicalRecord
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

        public bool UpdateMedicalRecord(MedicalRecord record)
        {
            var mr = GetMedicalRecord(record.MedicalRecordId);
            if (mr == null)
            {
                return false;
            }
            context.MedicalRecords.Update(record);
            context.SaveChanges();
            return true;
        }

        public bool AddMedicalRecord(MedicalRecord record)
        {
            var mr = GetMedicalRecord(record.MedicalRecordId);
            if (mr != null)
            {
                return false;
            }
            context.MedicalRecords.Add(record);
            context.SaveChanges();
            return true;
        }

        /*public void AddServiceMR(ServiceMedicalRecord sm)
        {
            context.Database.ExecuteSqlRaw("INSERT INTO [ServiceMedicalRecord]" +
                "          ([serviceId]" +`
                "           ,[medicalRecordId])" +
                "     VALUES" +
                "          (" + sm.Sid + " ," + sm.Mid + ")");
            context.SaveChanges();
        }

        public List<ServiceMedicalRecord> GetServiceUses()
        {
            return null;
        }*/

        public string DeleteMedicalRecord(int id)
        {
            /* 0 - medical record does not exist
               1 - delete success
              -1 - can't delete cause patient is already treatment
             */
            var mr = GetMedicalRecord(id);

            if (mr.ExaminationResultIds.Any())
            {
                return "-1";
            }
            else
            {
                context.MedicalRecords.Remove(mr);
                context.SaveChanges();
                return "1";
            }
        }

    }
}
