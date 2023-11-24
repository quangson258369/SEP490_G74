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
            var output = new List<Patient>();
            /*var db = new HcsContext();
            
            var patientIds = db.Patients.Select(patient => patient.PatientId).ToList();
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

        public Patient GetPatientById(int id)
        {
            return context.Patients.FirstOrDefault(x => x.PatientId == id);
        }

        public List<Patient> SearchPatient(string name, DateTime date)
        {
            return context.Patients.FromSqlRaw("select * from Patient p " +
               " join Contact c on p.patientId = c.patientId " +
               " where c.Name like '%%' OR p.examDate = ''").ToList();
        }

        public bool UpdatePatient(Patient p)
        {
            var existingPatient = context.Set<Patient>().Find(p.PatientId);
            var patient = GetPatientById(p.PatientId);
            if (patient == null)
            {
                return false;
            }

            if(existingPatient == null)
            {
                context.Patients.Update(p);
            }
            context.Entry(existingPatient).CurrentValues.SetValues(p);
            context.SaveChanges();
            UpdateContactForPatient(p.Contacts);
            return true;
        }

        public void UpdateContactForPatient(ICollection<Contact> contacts)
        {
            context.Contacts.UpdateRange(contacts);
            context.SaveChanges();
        }

        public bool AddPatient(Patient p)
        {
            var patient = GetPatientById(p.PatientId);
            if (patient != null)
            {
                return false;
            }
            context.Patients.Add(p);
            context.SaveChanges();
            addContactForPatient(p);
            return true;
        }

        public void addContactForPatient(Patient p)
        {
            var contact = context.Contacts.Where(x => x.PatientId == p.PatientId);
            if (!contact.Any())
            {
                context.Contacts.AddRange(p.Contacts);
                context.SaveChanges();
            }
        }

        public string DeletePatient(int id)
        {
            var patient = GetPatientById(id);
            if (patient == null)
            {
                return "0";
            }else if (patient.Invoices.Any())
            {
                return "-1";
            }
            context.Patients.Remove(patient);
            context.SaveChanges();
            return "1";
        }
    }
}
