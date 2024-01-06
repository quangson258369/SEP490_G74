using HCS.Domain.CustomExceptions;
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

    public async Task<MedicalRecord?> GetMrById(int id)
    {
        IQueryable<MedicalRecord> query = _dbSet;
        var mrById = await query
            .Where(med => med!.MedicalRecordId == id)
            .Include(c => c!.MedicalRecordCategories!)
                .ThenInclude(c => c!.Category!)
            .Include(c => c.MedicalRecordDoctors!)
                .ThenInclude(c => c.Doctor!)
                    .ThenInclude(c => c!.Contact)
            .Include(c => c!.ServiceMedicalRecords!)
                .ThenInclude(x=>x.Service)
            .FirstOrDefaultAsync();
        return mrById;
    }

    public Task<List<MedicalRecord>> GetMRByPatientId(int patientId)
    {
        var listMrByPatientId = _context.MedicalRecords
            .Where(med => med.PatientId == patientId)
            .Include(c => c.Patient)
            .ThenInclude(n => n.Contact)
            .ToListAsync();
            
        return listMrByPatientId;
    }

    public async Task UpdateMrStatusToCheckUp(int mrId)
    {
        var mr = await _context.MedicalRecords.FindAsync(mrId);
        if(mr != null)
        {
            if(mr.IsPaid == false) 
                throw new MedicalRecordNotPaidBeforeCheckUpException("Medical record is not paid yet");
            mr.IsCheckUp = true;
        }
    }

    public async Task UpdateMrStatusToPaid(int mrId)
    {
        var mr = await _context.MedicalRecords.FindAsync(mrId);
        if (mr != null)
        {
            mr.IsPaid = true;
        }
    }
}