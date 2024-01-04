using HCS.ApplicationContext;
using HCS.DataAccess.IRepository;
using HCS.Domain.Models;

namespace HCS.DataAccess.Repository;

public interface IServiceRepo : IGenericRepo<Service>
{
    
}
public class ServiceRepo : GenericRepo<Service>, IServiceRepo
{
    public ServiceRepo(HCSContext context) : base(context)
    {
    }
}