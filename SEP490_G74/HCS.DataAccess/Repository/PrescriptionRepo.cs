using HCS.ApplicationContext;
using HCS.DataAccess.IRepository;
using HCS.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HCS.DataAccess.Repository;

public interface IPrescriptionRepo : IGenericRepo<Prescription>
{
    Task<bool> IsPresSameCategoryWithDoctor(int prescriptionId, int doctorId);
}
public class PrescriptionRepo : GenericRepo<Prescription>, IPrescriptionRepo
{
    public PrescriptionRepo(HCSContext context) : base(context)
    {
    }

    public async Task<bool> IsPresSameCategoryWithDoctor(int prescriptionId, int doctorId)
    {
        var doctor = _context.Users.Find(doctorId);
        if(doctor != null)
        {
            var pres = await _context.Prescriptions
                .Include(p => p.ExaminationResult)
                .ThenInclude(e => e.MedicalRecord)
                .ThenInclude(m => m.MedicalRecordCategories)
                .FirstOrDefaultAsync(p => p.PrescriptionId == prescriptionId);

            if(pres != null)
            {
                if(pres.ExaminationResult!.MedicalRecord!.MedicalRecordCategories!.Any(m => m.CategoryId == doctor.CategoryId))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        return false;
    }
}