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
    internal class ServiceTypeConfig : IEntityTypeConfiguration<ServiceType>
    {
        public void Configure(EntityTypeBuilder<ServiceType> builder)
        {
            builder
                .HasMany(c => c.Services)
                .WithOne(c => c.ServiceType)
                .HasForeignKey(c => c.ServiceTypeId);

            builder
                .HasData(
                    new ServiceType { ServiceTypeId = 1, ServiceTypeName = "Khám tổng quát", CategoryId = 1 },
                    new ServiceType { ServiceTypeId = 2, ServiceTypeName = "Khám chuyên khoa", CategoryId = 1 },
                    new ServiceType { ServiceTypeId = 3, ServiceTypeName = "Khám nội soi", CategoryId = 2 },
                    new ServiceType { ServiceTypeId = 4, ServiceTypeName = "Chẩn đoán hình ảnh", CategoryId = 2 },
                    new ServiceType { ServiceTypeId = 5, ServiceTypeName = "Xét nghiệm", CategoryId = 3 },
                    new ServiceType { ServiceTypeId = 6, ServiceTypeName = "Phẫu thuật", CategoryId = 3 }
                );
        }
    }
}
