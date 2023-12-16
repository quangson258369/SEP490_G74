using Azure;
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

        public List<MedicalRecord> ListMRByPatient(int pid)
        {
            var output = context.MedicalRecords.Where(x => x.PatientId == pid).ToList();
            return output;
        }

        public List<Patient> ListPatientPaging(int page = 1)
        {
            int pageSize = 5;
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

        public List<Patient> SearchPatient(string name, int page = 1)
        {
            int pageSize = 5;
            if (name == null)
            {
                var output = context.Patients.ToList();
                var pagedData = output.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return pagedData;
            }
            else
            {
                var output = context.Patients.FromSqlRaw("select p.* from Patient p " +
                               " join Contact c on p.patientId = c.patientId " +
                               " where c.Name like '%'+ {0} +'%' OR c.Phone like '%' + {0} + '%'", name).ToList();
                var pagedData = output.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return pagedData;
            }
        }

        public bool UpdatePatient(Patient p)
        {
            var patient = GetPatientById(p.PatientId);
            if (patient == null)
            {
                return false;
            }
            context.Entry(patient).CurrentValues.SetValues(p);
            context.SaveChanges();
            return true;
        }

        public bool UpdateContactForThisP(Contact c)
        {
            var contact = context.Contacts.Where(x => x.CId == c.CId);
            if (contact.Any())
            {
                context.Contacts.RemoveRange(contact);
                context.SaveChanges();
                addContactForPatient(c);
                return true;
            }
            return false;
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
            return true;
        }

        public bool addContactForPatient(Contact p)
        {
            var contact = context.Contacts.Where(x => x.CId == p.CId);
            if (!contact.Any())
            {
                context.Contacts.Add(p);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public string DeletePatient(int id)
        {
            var patient = GetPatientById(id);
            if (patient == null)
            {
                return "0";
            }
            else if (patient.Invoices.Any())
            {
                return "-1";
            }
            context.Patients.Remove(patient);
            context.SaveChanges();
            return "1";
        }
    }
}
