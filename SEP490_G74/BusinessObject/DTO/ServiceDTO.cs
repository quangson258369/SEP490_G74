using API.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.DTO
{
    public class ServiceDTO:BaseOutputCommon
    {
        public int ServiceId { get; set; }

        public int ServiceTypeId { get; set; }

        public string ServiceName { get; set; } = null!;
    }
}
