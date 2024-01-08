using HCS.ApplicationContext;
using HCS.DataAccess.IRepository;
using HCS.Domain.Models;

namespace HCS.DataAccess.Repository;

public interface IExaminationResultRepo : IGenericRepo<ExaminationResult>
{
    
}
public class ExaminationResultRepo : GenericRepo<ExaminationResult>, IExaminationResultRepo
{
    public ExaminationResultRepo(HCSContext context) : base(context)
    {
        
    }
}