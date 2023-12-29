using HCS.ApplicationContext;
using HCS.DataAccess.IRepository;
using HCS.Domain.Models;

namespace HCS.DataAccess.Repository;

public interface ISuppliesTypeRepo : IGenericRepo<SuppliesType>
{
    
}
public class SuppliesTypeRepo : GenericRepo<SuppliesType>, ISuppliesTypeRepo
{
    public SuppliesTypeRepo(HCSContext context) : base(context)
    {
    }
} 