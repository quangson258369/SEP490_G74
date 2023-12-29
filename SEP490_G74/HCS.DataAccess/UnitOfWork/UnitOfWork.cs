using HCS.ApplicationContext;
using HCS.DataAccess.IRepository;
using HCS.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HCSContext _context;

        public IUserRepo UserRepo { get; }

        public IPatientRepo PatientRepo { get; }

        public IContactRepo ContactRepo { get; }
        
        public IMedicalRecordRepo MedicalRecordRepo { get; }
        public ICategoryRepo CategoryRepo { get; }
        public ISuppliesTypeRepo SuppliesTypeRepo { get; }

        public UnitOfWork(HCSContext context)
        {
            _context = context;
            UserRepo = new UserRepo(context);
            PatientRepo = new PatientRepo(context);
            ContactRepo = new ContactRepo(context);
            MedicalRecordRepo = new MedicalRecordRepo(context);
            CategoryRepo = new CategoryRepo(context);
            SuppliesTypeRepo = new SuppliesTypeRepo(context);
        }

        public async Task SaveChangeAsync()
        {
          await _context.SaveChangesAsync();
        }
    }
}
