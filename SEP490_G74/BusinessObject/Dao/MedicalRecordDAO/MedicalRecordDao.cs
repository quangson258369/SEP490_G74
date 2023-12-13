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
        
        

        public List<MedicalRecord> MedicalRecordListPaging( int page = 1)
        {
            int pageSize = 3;
            var query = context.MedicalRecords.ToList();
            var pagedData = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return pagedData;
        }

        public int GetCountOfListMR()
        {
            return context.MedicalRecords.Count();
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
                    ServiceMedicalRecords = x.ServiceMedicalRecords
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

        public List<Employee> GetDoctorByServiceType(int type)
        {
            if( type!= null || type == 0)
            {
                return context.Employees.Where(x=>x.ServiceTypeId == type).ToList();
            }
            return new List<Employee>();
        }

        public void AddServiceMR(ServiceMedicalRecord sm)
        {
            if (sm != null && !context.ServiceMedicalRecords.Any(x=>x.MedicalRecordId == sm.MedicalRecordId && x.ServiceId == sm.ServiceId))
            {
                sm.Status = false;
                context.ServiceMedicalRecords.Add(sm);
                context.SaveChanges();
            }
        }

        public void EditServiceMR (List<ServiceMedicalRecord> list,int mrid)
        {
            List<ServiceMedicalRecord> listuse = GetServiceUses(mrid);
            if (listuse.Any())
            {
                context.ServiceMedicalRecords.RemoveRange(listuse);
                context.SaveChanges();
            }

            if(list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    item.Status = false;
                }
                context.ServiceMedicalRecords.AddRange(list);
                context.SaveChanges();
            }
                
        }

        public List<ServiceMedicalRecord> GetServiceUses(int id)
        {
            var list = context.ServiceMedicalRecords.Where(x=>x.MedicalRecordId == id).ToList();
            return list;
        }

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
