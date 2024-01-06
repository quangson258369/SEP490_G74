using HCS.Domain.Models;

namespace HCS.DataAccess.IRepository;

public interface IMedicalRecordRepo : IGenericRepo<MedicalRecord>
{
    Task<List<MedicalRecord>> GetMRByPatientId(int patientId);
    Task<MedicalRecord?> GetMrById(int id);
    Task UpdateMrStatusToPaid(int mrId);
    Task UpdateMrStatusToCheckUp(int mrId);
}