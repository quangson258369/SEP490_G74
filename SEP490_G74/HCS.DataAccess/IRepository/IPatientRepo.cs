using HCS.Domain.Models;

namespace HCS.DataAccess.IRepository
{
    public interface IPatientRepo : IGenericRepo<Patient>
    {
        Task<List<Patient>> GetPatients(int userId);
        Task<Patient?> GetPatientByUserId(int userId);
    }
}
