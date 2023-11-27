using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.DTO
{
    public class DoctorMRDTO
    {
        public int DoctorId { get; set; }

        public int ServiceTypeId { get; set; }

        public int UserId { get; set; }

        public ContactDoctorDTO? Contact { get; set; }
    }
}
