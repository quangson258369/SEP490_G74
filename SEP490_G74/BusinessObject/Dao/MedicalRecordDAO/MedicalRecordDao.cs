using DataAccess.Entity;
using HcsBE.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.Dao.MedicalRecordDAO
{
    public class MedicalRecordDao
    {
        private HcsContext context = new HcsContext();

        public List<MedicalRecord> MedicalRecordList()
        {
            var query = context.MedicalRecords.ToList();
            return query;
        }
        public List<Invoice> GetInvoiceList()
        {
            var list = context.Invoices.ToList();
            return list;
        }

        public bool AddToInvoice(Invoice invoice)
        {
            if (invoice == null)
            {
                return false;
            }
            context.Invoices.Add(invoice);
            context.SaveChanges();
            return true;
        }

        public bool AddToInvoiceDetail(InvoiceDetail invoice)
        {
            if (invoice == null)
            {
                return false;
            }
            else
            {
                var invoicee = context.Invoices.ToList();
                if (invoicee == null)
                {
                    return false;
                }
                else
                {
                    int invoiceId = invoicee.Last().InvoiceId;
                    var invd = context.InvoiceDetails.ToList();
                    int detailid = ((invd == null || invd.Count == 0) ? 1 : invd.Max(s => s.InvoiceDetailId) + 1);
                    InvoiceDetail detail = new InvoiceDetail()
                    {
                        InvoiceDetailId = detailid,
                        InvoiceId = invoiceId,
                        MedicalRecordId = invoice.MedicalRecordId,
                        IsPrescription = false
                    };
                    context.InvoiceDetails.Add(detail);
                    context.SaveChanges();
                    return true;
                }
            }
        }

        public List<User> GetListCashier()
        {
            var query = context.Users.FromSqlRaw("select u.* from [User] u " +
                "join UserRole ur on ur.userId = u.userId " +
                "where ur.roleId = 3").ToList();
            return query;
        }

        public List<MedicalRecord> searchMR(string str, int page)
        {
            int pageSize = 4;
            if (str == null || str.Length == 0)
            {
                var query = context.MedicalRecords.ToList();
                var pagedData = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return pagedData;
            }
            else
            {
                var output = context.MedicalRecords.FromSqlRaw("select mr.* from MedicalRecord mr " +
                                "join Patient p on p.patientId = mr.patientId " +
                                "join Contact c on p.patientId = c.patientId " +
                                "where c.Name like '%'+ {0} +'%' or c.Phone like '%'+ {0} +'%'", str).ToList();
                var pagedData = output.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return pagedData;
            }
        }

        public List<MedicalRecord> MedicalRecordListPaging(int page = 1)
        {
            int pageSize = 4;
            var query = context.MedicalRecords.ToList();
            var pagedData = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return pagedData;
        }

        public int GetCountOfListMR()
        {
            return context.MedicalRecords.Count();
        }

        public MedicalRecord GetMedicalRecord(int id)
        {
            var query = context.MedicalRecords
                .Select(x => new MedicalRecord
                {
                    Doctor = x.Doctor,
                    DoctorId = x.DoctorId,
                    ExamCode = x.ExamCode,
                    ExaminationResultIds = x.ExaminationResultIds,
                    ExamReason = x.ExamReason,
                    MedicalRecordDate = x.MedicalRecordDate,
                    MedicalRecordId = x.MedicalRecordId,
                    Patient = x.Patient,
                    PatientId = x.PatientId,
                    ServiceMedicalRecords = x.ServiceMedicalRecords
                }).SingleOrDefault(x => x.MedicalRecordId == id);
            return query;
        }

        public bool UpdateMedicalRecord(MedicalRecord record)
        {
            var mr = GetMedicalRecord(record.MedicalRecordId);
            if (mr == null)
            {
                return false;
            }
            context.MedicalRecords.Update(record);
            context.SaveChanges();
            return true;
        }

        public bool AddMedicalRecord(MedicalRecord record)
        {
            var mr = GetMedicalRecord(record.MedicalRecordId);
            if (mr != null)
            {
                return false;
            }
            context.MedicalRecords.Add(record);
            context.SaveChanges();
            return true;
        }

        public List<Employee> GetDoctorByServiceType(int type)
        {
            if (type != 0)
            {
                return context.Employees.Where(x => x.ServiceTypeId == type).ToList();
            }
            return new List<Employee>();
        }

        public void AddServiceMR(ServiceMedicalRecord sm)
        {
            if (sm != null && !context.ServiceMedicalRecords.Any(x => x.MedicalRecordId == sm.MedicalRecordId && x.ServiceId == sm.ServiceId))
            {
                sm.Status = false;
                context.ServiceMedicalRecords.Add(sm);
                context.SaveChanges();
            }
        }

        public void EditServiceMR(List<ServiceMedicalRecord> list, int mrid)
        {
            List<ServiceMedicalRecord> listuse = GetServiceUses(mrid);
            if (listuse.Any())
            {
                context.ServiceMedicalRecords.RemoveRange(listuse);
                context.SaveChanges();
            }

            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    item.Status = false;
                }
                context.ServiceMedicalRecords.AddRange(list);
                context.SaveChanges();
            }

        }

        public List<ServiceMedicalRecord> GetServiceUses(int id)
        {
            var list = context.ServiceMedicalRecords.Where(x => x.MedicalRecordId == id).ToList();
            return list;
        }

        public string DeleteMedicalRecord(int id)
        {
            /* 0 - medical record does not exist
               1 - delete success
              -1 - can't delete cause patient is already treatment
             */
            var mr = GetMedicalRecord(id);

            if (mr.ExaminationResultIds.Any())
            {
                return "-1";
            }
            else
            {
                context.MedicalRecords.Remove(mr);
                context.SaveChanges();
                return "1";
            }
        }

    }
}
