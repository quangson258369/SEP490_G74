using HCS.ApplicationContext;
using HCS.DataAccess.IRepository;
using HCS.Domain.Models;

namespace HCS.DataAccess.Repository;


public interface ISuppliesRepo : IGenericRepo<Supply>
{
    
}
public class SuppliesRepo : GenericRepo<Supply>, ISuppliesRepo
{
    public SuppliesRepo(HCSContext context) : base(context)
    {
    }
}