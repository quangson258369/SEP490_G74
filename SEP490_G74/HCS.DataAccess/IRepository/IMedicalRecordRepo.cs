using HCS.Domain.Models;

namespace HCS.DataAccess.IRepository;

public interface IMedicalRecordRepo : IGenericRepo<MedicalRecord>
{
    Task<List<MedicalRecord>> GetMRByPatientId(int patientId);
    Task<MedicalRecord?> GetMrById(int id);
    Task<MedicalRecord?> GetMrForPrescriptionByMedicalRecordId(int id);
    Task UpdateMrStatusToPaid(int mrId);
    Task UpdateMrStatusToPaid(int mrId, int? userId);
    Task UpdateMrStatusToCheckUp(int mrId);
}