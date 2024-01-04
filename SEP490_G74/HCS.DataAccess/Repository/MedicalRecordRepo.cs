using HCS.ApplicationContext;
using HCS.DataAccess.IRepository;
using HCS.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HCS.DataAccess.Repository;

public class MedicalRecordRepo : GenericRepo<MedicalRecord>, IMedicalRecordRepo
{
    public MedicalRecordRepo(HCSContext context) : base(context)
    {
    }

    public Task<List<MedicalRecord>> GetMRByPatientId(int patientId)
    {
        var listMrByPatientId = _context.MedicalRecords
            .Where(med => med.PatientId == patientId)
            .Include(c => c.Patient)
            .ThenInclude(n => n.Contacts.Select(na =>na.Name))
            .ToListAsync();
            
        return listMrByPatientId;
    }
}