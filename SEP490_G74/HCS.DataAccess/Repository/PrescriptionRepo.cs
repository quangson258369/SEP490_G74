using HCS.ApplicationContext;
using HCS.DataAccess.IRepository;
using HCS.Domain.Models;

namespace HCS.DataAccess.Repository;

public interface IPrescriptionRepo : IGenericRepo<Prescription>
{
    
}
public class PrescriptionRepo : GenericRepo<Prescription>, IPrescriptionRepo
{
    public PrescriptionRepo(HCSContext context) : base(context)
    {
    }
}