using HCS.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS.ApplicationContext.Configurations
{
    internal class ServiceConfig : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder
                .HasData(
                    new Service { ServiceId = 1, ServiceName = "Tiêm vaccine", ServiceTypeId = 1 },
                    new Service { ServiceId = 2, ServiceName = "Khám mắt", ServiceTypeId = 1 },
                    new Service { ServiceId = 3, ServiceName = "Khám đại tràng", ServiceTypeId = 2 },
                    new Service { ServiceId = 4, ServiceName = "Siêu âm màu", ServiceTypeId = 2 },
                    new Service { ServiceId = 5, ServiceName = "Xét nghiệm máu", ServiceTypeId = 3 },
                    new Service { ServiceId = 6, ServiceName = "Phẫu thuật cắt ruột thừa", ServiceTypeId = 3 },
                    new Service { ServiceId = 7, ServiceName = "Tiêm vaccine cúm", ServiceTypeId = 4 },
                    new Service { ServiceId = 8, ServiceName = "Khám tai mũi họng", ServiceTypeId = 4 },
                    new Service { ServiceId = 9, ServiceName = "Khám dạ dày", ServiceTypeId = 5 },
                    new Service { ServiceId = 10, ServiceName = "Siêu âm", ServiceTypeId = 5 },
                    new Service { ServiceId = 11, ServiceName = "Xét nghiệm nước tiểu", ServiceTypeId = 6 },
                    new Service { ServiceId = 12, ServiceName = "Phẫu thuật nối gân tay", ServiceTypeId = 6 }
                );
        }
    }
}
