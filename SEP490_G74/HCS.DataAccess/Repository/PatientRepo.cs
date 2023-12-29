﻿using HCS.ApplicationContext;
using HCS.DataAccess.IRepository;
using HCS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HCS.DataAccess.Repository
{
    public class PatientRepo : GenericRepo<Patient>, IPatientRepo
    {
        public PatientRepo(HCSContext context) : base(context)
        {
        }


        public async Task<List<Patient>> GetPatients(int userId)
        {
            //Get doctorId by userId from token
            var doctor =await _context.Users.FirstOrDefaultAsync(e => e.UserId == userId);

            if (doctor == null || doctor.UserId < 0) throw new Exception();

            //Get MedicalRecords by doctorId and categoryId
            var patientIds = await _context.MedicalRecords
                .Where(med => med.DoctorId == doctor.UserId && med.CategoryId == doctor.CategoryId)
                .Select(m => m.PatientId)
                .ToListAsync();

            //Get patients from patientId list
            var output = await _context.Patients
                .Where(patient => patientIds
                    .Contains(patient.PatientId))
                .ToListAsync();
            return output;
        }
    }
}