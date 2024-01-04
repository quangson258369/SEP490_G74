using HCS.ApplicationContext;
using HCS.DataAccess.IRepository;
using HCS.Domain.Models;

namespace HCS.DataAccess.Repository;

public interface IServiceTypeRepo : IGenericRepo<ServiceType>
{
    
}
public class ServiceTypeRepo : GenericRepo<ServiceType>, IServiceTypeRepo
{
    public ServiceTypeRepo(HCSContext context) : base(context)
    {
    }
}