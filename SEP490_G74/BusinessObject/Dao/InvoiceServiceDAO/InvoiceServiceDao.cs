using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HcsBE.Dao.InvoiceServiceDAO
{
    public class InvoiceServiceDao
    {
        private HcsContext context = new HcsContext();

        string listSql = "select i.*  from Invoice i " +
            "join InvoiceDetail id on id.invoiceId = i.invoiceId " +
            "where id.isPrescription = 0 and i.cashierId = ";
        public List<Invoice> getListInvoiceByCashier(int page ,int uid)
        {
            int pageSize = 4;
            var list = context.Invoices.FromSqlRaw(listSql += uid.ToString()).ToList();
            var pagedData = list.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return pagedData;
        }

        public List<Invoice> getSearchPagingInvoice(int page ,int uid, string str, int status)
        {
            int pageSize = 4;
            if((str == null || str.Length == 0) && status == -1)
            {
                var list = context.Invoices.FromSqlRaw(listSql += uid.ToString()).ToList();
                var pagedData = list.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return pagedData;
            }else if(status == -1 && str != null && str.Length > 0)
            {
                var list = context.Invoices.FromSqlRaw("select i.*  from Invoice i " +
                    "join InvoiceDetail id on id.invoiceId = i.invoiceId " +
                    "join Patient p on p.patientId = i.patientId " +
                    "join Contact c on p.patientId = c.patientId " +
                    "where id.isPrescription = 0 and i.cashierId = {1} and (c.[Name] like '%{0}%' or c.Phone like '%{0}%')",str,uid).ToList();
                var pagedData = list.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return pagedData;
            }else if(status != -1 && str == null || str.Length == 0)
            {
                var list = context.Invoices.FromSqlRaw("select i.*  from Invoice i " +
                    "join InvoiceDetail id on id.invoiceId = i.invoiceId " +
                    "join Patient p on p.patientId = i.patientId " +
                    "join Contact c on p.patientId = c.patientId " +
                    "where id.isPrescription = 0 and i.cashierId = {1} and i.status = {0}", status, uid).ToList();
                var pagedData = list.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return pagedData;
            }
            else
            {
                var list = context.Invoices.FromSqlRaw("select i.*  from Invoice i " +
                    "join InvoiceDetail id on id.invoiceId = i.invoiceId " +
                    "join Patient p on p.patientId = i.patientId " +
                    "join Contact c on p.patientId = c.patientId " +
                    "where id.isPrescription = 0 and i.cashierId = {1} and i.status = {0} " +
                    "and (c.[Name] like '%{2}%' or c.Phone like '%{2}%')", status, uid, str).ToList();
                var pagedData = list.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return pagedData;
            }
        }


    }
}
