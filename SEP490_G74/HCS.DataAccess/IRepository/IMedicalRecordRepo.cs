using HCS.Domain.Models;

namespace HCS.DataAccess.IRepository;

public interface IMedicalRecordRepo : IGenericRepo<MedicalRecord>
{
    Task<List<MedicalRecord>> GetMRByPatientId(int patientId);
}