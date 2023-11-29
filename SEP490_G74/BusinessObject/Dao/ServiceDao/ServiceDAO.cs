using AutoMapper.Configuration.Conventions;
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
                .ThenInclude(x=>x.Employees).ToList();
        }
        public List<Service> SearchService(string name, int typeId)
        {
            return context.Services.Include(x=>x.ServiceSupplies)
                .Include(x=>x.ServiceType)
                .ThenInclude(x=>x.Employees).Where(x=>x.ServiceName == name && x.ServiceTypeId == typeId).ToList();
        }

        public List<ServiceType> GetListServiceType()
        {
            return context.ServiceTypes.ToList();
        }

        public Service GetService(int serviceId)
        {
            return (Service)context.Services.Include(x => x.ServiceSupplies)
                .Include(x => x.ServiceType)
                .ThenInclude(x => x.Employees).Where(p => p.ServiceId.Equals(serviceId));
        }

        public bool UpdateService(Service service)
        {
            var existingPatient = context.Set<Service>().Find(service.ServiceId);
            var services = GetService(service.ServiceId);
            if(services == null) return false;
            if (existingPatient == null)
            {
                context.Services.Update(service);
            }
            context.Entry(existingPatient).CurrentValues.SetValues(service);
            context.SaveChanges();
            return true;
        }

        public bool AddService(Service service)
        {
            var services = GetService(service.ServiceId);
            if (services != null) return false;
            context.Services.Add(service);
            context.SaveChanges();
            return true;
        }

        public bool DeleteService(int id)
        {
            try
            {
                var services = GetService(id);
                if (services == null) return false;
                context.Services.Remove(services);
                context.SaveChanges();
                return true;
            }catch (Exception ex)
            {
                Console.WriteLine("Delete Service fail cause: \n"+ex);
                return false;
            }
        }
    }
}
