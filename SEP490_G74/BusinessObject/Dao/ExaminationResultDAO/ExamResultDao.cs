using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.Dao.ExaminationResultDAO
{
    public class ExamResultDao
    {
        private HcsContext context = new HcsContext();
        public List<ExaminationResultId> GetList()
        {
            return context.ExaminationResultIds.Include(x=>x.MedicalRecord).ToList();
        }

        public List<ServiceMedicalRecord> ListServiceMRByServiceType(int type)
        {
            var query = context.ServiceMedicalRecords.FromSqlRaw("select smr.* from MedicalRecord mr " +
                "join ServiceMedicalRecord smr on mr.medicalRecordID = smr.medicalRecordId " +
                "join [Service] s on smr.serviceId = s.serviceId " +
                "where s.serviceTypeId = " + type + " and smr.status = 0").ToList();
            return query;
        }

        public bool UpdateStatusOfServiceMedicalRecord(int sid,int mrid)
        {
            var status = context.ServiceMedicalRecords.FromSqlRaw("UPDATE [ServiceMedicalRecord] " +
                "   SET [status] = 1 " +
                " WHERE serviceId = "+sid+" AND medicalRecordId = "+mrid+"\n");
            context.SaveChanges();
            if(status != null)
            {
                return true;
            }
            return false;
        }

        public ExaminationResultId GetExamResult(int id)
        {
            return context.ExaminationResultIds.Include(x => x.MedicalRecord).SingleOrDefault(x => x.ExamResultId == id);
        }

        public bool Update(ExaminationResultId exam)
        {
            var er = GetExamResult(exam.ExamResultId);
            if (er == null)
            {
                return false;
            }
            context.ExaminationResultIds.Update(exam);
            context.SaveChanges();
            return true;
        }

        public bool AddExamResult(ExaminationResultId exam)
        {
            var er = GetExamResult(exam.ExamResultId);
            if (er != null)
            {
                return false;
            }
            context.ExaminationResultIds.Add(exam);
            context.SaveChanges();
            return true;
        }

        public bool DeleteMedicalRecord(int id)
        {
            var er = GetExamResult(id);
            if (er == null)
            {
                return false;
            }
            context.ExaminationResultIds.Remove(er);
            context.SaveChanges();
            return true;
        }
    }
}
