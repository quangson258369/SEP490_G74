using HCS.DataAccess.IRepository;
using HCS.DataAccess.Repository;

namespace HCS.DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IUserRepo UserRepo { get; }
        public IPatientRepo PatientRepo { get; }
        public IContactRepo ContactRepo { get; }
        public IMedicalRecordRepo MedicalRecordRepo { get; }
        public ICategoryRepo CategoryRepo { get; }
        public ISuppliesTypeRepo SuppliesTypeRepo { get; }
        public IPrescriptionRepo PrescriptionRepo { get; }
        public IRoleRepo RoleRepo { get; }
        public IServiceTypeRepo ServiceTypeRepo { get; }
        public IServiceRepo ServiceRepo { get; }
        public IExaminationResultRepo ExaminationResultRepo { get; }
        public ISuppliesRepo SuppliesRepo { get; }
        public ISuppliesPrescriptionRepo SuppliesPrescriptionRepo { get; }

        public Task SaveChangeAsync();
    }
}