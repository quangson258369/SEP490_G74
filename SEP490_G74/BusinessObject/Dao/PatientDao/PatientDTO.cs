using API.Common;
using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.Dao.PatientDao
{
    public class PatientDTO:BaseOutputCommon
    {
        public int PatientId { get; set; }

        public string ?ServiceDetailName { get; set; }

        public DateTime ExamDate { get; set; }

        public virtual ICollection<Contact>? Contacts { get; set; }
        
        public virtual ICollection<Invoice> ?Invoices { get; set; }
        
        public virtual ICollection<MedicalRecord> ?MedicalRecords { get; set; }
    }
}
