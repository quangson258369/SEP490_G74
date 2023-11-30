using API.Common;
using AutoMapper;
using DataAccess.Entity;
using HcsBE.Dao.ServiceDao;
using HcsBE.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.Bussiness.ServiceBusiness
{
    public class ServiceLogic
    {
        private ServiceDAO dao = new ServiceDAO();
        private IMapper mapper;

        public ServiceLogic(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public List<ServiceType> GetServiceTypes()
        {
            return dao.GetListServiceType();
        }

        public List<ServiceDTO> GetAll()
        {
            List<Service> services = dao.ListService();
            var output = mapper.Map<List<ServiceDTO>>(services);
            if (services == null)
            {
                return new List<ServiceDTO>()
                {
                    new ServiceDTO()
                    {
                        ExceptionMessage = ConstantHcs.NotFound,
                        ResultCd = ConstantHcs.BussinessError
                    }
                };
            }
            return output;
        }

        public ServiceDTO GetService(int id)
        {
            Service service = dao.GetService(id);
            var output = mapper.Map<ServiceDTO>(service);
            if (service == null)
            {
                return new ServiceDTO()
                    {
                        ExceptionMessage = ConstantHcs.NotFound,
                        ResultCd = ConstantHcs.BussinessError
                    };
            }
            return output;
        }

        public bool Update(ServiceDTO service)
        {
            var s = mapper.Map<Service>(service);
            var status = dao.UpdateService(s);
            return status;
        }
        
        public bool Add(ServiceDTO service)
        {
            var s = mapper.Map<Service>(service);
            var status = dao.AddService(s);
            return status;
        }

        public bool Delete(int id)
        {
            if(id == 0 || id == null) return false;
            var service = dao.GetService(id);
            if(service == null) return false;
            var output = dao.DeleteService(id);
            return output;
        }

        public List<ServiceDTO> SearchService(string name, int typeId)
        {
            List<Service> services = dao.SearchService(name,typeId);
            var output = mapper.Map<List<ServiceDTO>>(services);
            if (services == null)
            {
                return new List<ServiceDTO>()
                {
                    new ServiceDTO()
                    {
                        ExceptionMessage = ConstantHcs.NotFound,
                        ResultCd = ConstantHcs.BussinessError
                    }
                };
            }
            return output;
        }

    }
}
