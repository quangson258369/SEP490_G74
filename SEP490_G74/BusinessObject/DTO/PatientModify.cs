using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.DTO
{
    public class PatientModify
    {
        public int PatientId { get; set; }

        public string? ServiceDetailName { get; set; }

        public DateTime ExamDate { get; set; }

        public ContactPatientDTO? Contact { get; set; }
    }
}
