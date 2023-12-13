using DataAccess.Entity;
using HcsBE.DTO;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.Dao.PrecriptionDAO
{
    public class PrecriptionDAO
    {
        private HcsContext context = new HcsContext();
        public int AddPrecription(PrescriptionDTO prescription)
        {
            Prescription newPrescription = new Prescription
            {
                CreateDate = prescription.CreateDate,
                Diagnose = prescription.Diagnose,
            };
            context.Prescriptions.Add(newPrescription);
            context.SaveChanges();
            var id = context.Prescriptions.Max(id => id.PrescriptionId);
            return id;
        }
        public bool AddSuppliesInPrescription(SuppliesPrescriptionDTO suppliesPrescription)
        {
            SuppliesPrescription newSuppliesPrescription = new SuppliesPrescription
            {
                PrescriptionId= suppliesPrescription.PrescriptionId,
                SId = suppliesPrescription.SId,
                Quantity= suppliesPrescription.Quantity,
            };
            context.SuppliesPrescriptions.Add(newSuppliesPrescription);
            context.SaveChanges();
            return true;
        }
        public List<PrescriptionInforDTO> GetListPresciptionInfors(int page=1,int idUser=0)
        {
            //get idDoctor
            int? doctorIdInt = context.Users
            .Where(user => user.UserId == idUser)
            .Join(context.Employees, user => user.UserId, employee => employee.UserId, (user, employee) => employee.DoctorId)
            .FirstOrDefault();
            Console.WriteLine(doctorIdInt);
            //list
            int pageSize = 3;
            var result = context.Patients
                .Join(context.Contacts, patient => patient.PatientId, contact => contact.PatientId, (patient, contact) => new { patient, contact })
                .Join(context.MedicalRecords, pc => pc.patient.PatientId, medicalRecord => medicalRecord.PatientId, (pc, medicalRecord) => new { pc, medicalRecord })
                .Where(doctorId => doctorId.medicalRecord.DoctorId== doctorIdInt)
                .Join(context.Prescriptions, pcrm => pcrm.medicalRecord.PrescriptionId, prescription => prescription.PrescriptionId, (pcrm, prescription) => new PrescriptionInforDTO
                {
                    PrescriptionId = prescription.PrescriptionId,
                    CreateDate = prescription.CreateDate,
                    Diagnose = prescription.Diagnose,
                    NamePatient = pcrm.pc.contact.Name,
                    PhonePatient = pcrm.pc.contact.Phone
                })
                .ToList();
            var pagedData = result.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return pagedData;
        }
        public int GetCountListPresciptionInfors()
        {
            int pageSize = 3;
            var result = context.Patients
                .Join(context.Contacts, patient => patient.PatientId, contact => contact.PatientId, (patient, contact) => new { patient, contact })
                .Join(context.MedicalRecords, pc => pc.patient.PatientId, medicalRecord => medicalRecord.PatientId, (pc, medicalRecord) => new { pc, medicalRecord })
                .Join(context.Prescriptions, pcrm => pcrm.medicalRecord.PrescriptionId, prescription => prescription.PrescriptionId, (pcrm, prescription) => new PrescriptionInforDTO
                {
                    PrescriptionId = prescription.PrescriptionId,
                    CreateDate = prescription.CreateDate,
                    Diagnose = prescription.Diagnose,
                    NamePatient = pcrm.pc.contact.Name,
                    PhonePatient = pcrm.pc.contact.Phone
                })
                .ToList();
            int count = result.Count();
            return count;
        }
        public List<PrescriptionInforDTO> GetListPresciptionInforsByIdPatient(int page =1,int idPatient=0)
        {
            int pageSize = 3;
            var result = context.Patients
                .Join(context.Contacts, patient => patient.PatientId, contact => contact.PatientId, (patient, contact) => new { patient, contact })
                .Where(patientId=>patientId.patient.PatientId==idPatient)
                .Join(context.MedicalRecords, pc => pc.patient.PatientId, medicalRecord => medicalRecord.PatientId, (pc, medicalRecord) => new { pc, medicalRecord })
                .Join(context.Prescriptions, pcrm => pcrm.medicalRecord.PrescriptionId, prescription => prescription.PrescriptionId, (pcrm, prescription) => new PrescriptionInforDTO
                {
                    PrescriptionId = prescription.PrescriptionId,
                    CreateDate = prescription.CreateDate,
                    Diagnose = prescription.Diagnose,
                    NamePatient = pcrm.pc.contact.Name,
                    PhonePatient = pcrm.pc.contact.Phone
                }).ToList();
            var pagedData = result.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return pagedData;
        }
        public ContactPatientInPrescriptionDTO GetPatientContactByPrescriptionId(int idPrescription)
        {
            var ContactPatients = context.Patients
            .Join(context.Contacts, p => p.PatientId, c => c.PatientId, (p, c) => new { Patient = p, Contact = c })
            .Join(context.MedicalRecords, pc => pc.Patient.PatientId, mr => mr.PatientId, (pc, mr) => new { pc.Patient, pc.Contact, MedicalRecord = mr })
            .Join(context.Prescriptions, m => m.MedicalRecord.PrescriptionId, pr => pr.PrescriptionId, (m, pr) => new { m.Patient, m.Contact, m.MedicalRecord, Prescription = pr })
            .Where(result => result.Prescription.PrescriptionId == idPrescription)
            .Select(result => new ContactPatientInPrescriptionDTO
            {
                PrescriptionId = result.Prescription.PrescriptionId,
                PatientId = result.Patient.PatientId,
                Name = result.Contact.Name,
                Gender = result.Contact.Gender,
                Phone = result.Contact.Phone,
                Dob = result.Contact.Dob,
                Address = result.Contact.Address,
                Img = result.Contact.Img
            })
            .SingleOrDefault();

            return ContactPatients;
        }
        public List<PrescriptionDetailSuppliesDTO> GetSuppliesByPrescriptionId(int idPrescription)
        {
            var listSuppliesInPrescription = context.Prescriptions
             .Where(p => p.PrescriptionId == idPrescription)
             .Join(context.SuppliesPrescriptions, p => p.PrescriptionId, sp => sp.PrescriptionId, (p, sp) => new { p, sp })
             .Join(context.Supplies, ps => ps.sp.SId, s => s.SId, (ps, s) => new { ps.p, ps.sp, s })
             .Join(context.SuppliesTypes, s => s.s.SuppliesTypeId, st => st.SuppliesTypeId, (s, st) => new PrescriptionDetailSuppliesDTO
             {
                 PrescriptionId = s.p.PrescriptionId,
                 CreateDate = s.p.CreateDate,
                 Diagnose = s.p.Diagnose,
                 SId = s.s.SId,
                 SuppliesName=s.s.SName,
                 Quantity = s.sp.Quantity,
                 SuppliesTypeName = st.SuppliesTypeName
             })
             .ToList();

            return listSuppliesInPrescription;
        }
    }
}
