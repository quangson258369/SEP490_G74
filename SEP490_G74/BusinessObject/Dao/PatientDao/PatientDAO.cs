using DataAccess.Entity;
using Microsoft.Data.SqlClient;
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
            var output = context.Patients.ToList();
            return output;
        }
        public List<Patient> ListPatientPaging(int page = 1)
        {
            int pageSize = 3;
            var output = context.Patients.ToList();
            var pagedData = output.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return pagedData;
        }

        public int GetCountOfListPatient()
        {
            return context.Patients.Count();
        }

        public Patient GetPatientById(int id)
        {
            return context.Patients.FirstOrDefault(x => x.PatientId == id);
        }

        public List<Patient> SearchPatient(string name)
        {
            return context.Patients.FromSqlRaw("select p.* from Patient p " +
               " join Contact c on p.patientId = c.patientId " +
               " where c.Name like '%'+ {0} +'%' OR p.serviceDetailName like '%' + {0} + '%'", name).ToList();
        }

        public bool UpdatePatient(Patient p)
        {
            UpdateContactForPatient(p.Contacts);
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
