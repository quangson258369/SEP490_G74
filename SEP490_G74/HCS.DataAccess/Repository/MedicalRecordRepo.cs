using HCS.ApplicationContext;
using HCS.DataAccess.IRepository;
using HCS.Domain.Models;

namespace HCS.DataAccess.Repository;

public class MedicalRecordRepo : GenericRepo<MedicalRecord>, IMedicalRecordRepo
{
    public MedicalRecordRepo(HCSContext context) : base(context)
    {
    }
}