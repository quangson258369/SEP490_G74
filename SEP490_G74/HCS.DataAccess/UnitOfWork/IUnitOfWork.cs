﻿using HCS.DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public Task SaveChangeAsync();
    }
}
