using API.Common;
using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.DTO
{
    public class PatientDTO : BaseOutputCommon
    {
        public int PatientId { get; set; }

        public string? ServiceDetailName { get; set; }

        public Contact? Contacts { get; set; }

        public List<Invoice>? Invoices { get; set; }

        public List<MedicalRecord>? MedicalRecords { get; set; }
    }
}
