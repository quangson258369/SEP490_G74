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
