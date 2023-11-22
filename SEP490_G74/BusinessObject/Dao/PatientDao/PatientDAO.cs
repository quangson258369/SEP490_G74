using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PatternContexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.Dao.PatientDao
{
    public class PatientDAO
    {
        private HcsContext context = new HcsContext();

        public List<Patient> ListPatients()
        {
            var db = new HcsContext();
            var output = new List<Patient>();

            /*var patientIds = db.Patients.Select(patient => patient.PatientId).ToList();

            var contacts = (from contact in db.Contacts
                            where patientIds.Contains(contact.PatientId.Value) 
                            select contact).ToList();

            var medicalRecords = (from medical in db.MedicalRecords
                                  where patientIds.Contains(medical.PatientId)
                                  select medical).ToList();

            var invoices = (from invoice in db.Invoices
                            where patientIds.Contains(invoice.PatientId)
                            select invoice).ToList();

            var query = (from patient in db.Patients
                         where patientIds.Contains(patient.PatientId)
                         select new
                         {
                             Patient = patient,
                             Contacts = contacts.Where(c => c.PatientId == patient.PatientId).ToList(),
                             MedicalRecords = medicalRecords.Where(m => m.PatientId == patient.PatientId).ToList(),
                             Invoices = invoices.Where(i => i.PatientId == patient.PatientId).ToList()
                         }).ToList();*/
            //output = query.Select(rs => rs.Patient).ToList();
            output = context.Patients.ToList();
            return output;
        }
    }
}
