using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.Dao.ServiceDao
{
    public class ServiceDAO
    {
        private HcsContext context = new HcsContext();

        public List<Service> ListService()
        {
            return context.Services.Include(x=>x.ServiceSupplies)
                .Include(x=>x.ServiceType)
                .ThenInclude(x=>x.Doctors).ToList();
        }

        public Service GetService(int serviceId)
        {
            return (Service)context.Services.Include(x => x.ServiceSupplies)
                .Include(x => x.ServiceType)
                .ThenInclude(x => x.Doctors).Where(p => p.ServiceId.Equals(serviceId));
        }

        public bool UpdateService(Service service)
        {
            return true;
        }
    }
}
