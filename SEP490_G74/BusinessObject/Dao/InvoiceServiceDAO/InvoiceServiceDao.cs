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

        public List<Invoice> getListInvoiceByCashier(int page, int uid)
        {
            int pageSize = 4;
            var list = context.Invoices.FromSqlRaw("select i.*  from Invoice i " +
                         " join InvoiceDetail id on id.invoiceId = i.invoiceId " +
                         " where id.isPrescription = 0 and i.cashierId = {0}", uid).ToList();
            var pagedData = list.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return pagedData;
        }

        public List<Invoice> GetFullListInvoice(int uid)
        {
            var list = context.Invoices.FromSqlRaw("select i.*  from Invoice i " +
                         " join InvoiceDetail id on id.invoiceId = i.invoiceId " +
                         " where id.isPrescription = 0 and i.cashierId = {0}", uid).ToList();
            return list;
        }

        public int GetInvoiceCount(int uid)
        {
            var list = context.Invoices.FromSqlRaw("select i.*  from Invoice i " +
                         " join InvoiceDetail id on id.invoiceId = i.invoiceId " +
                         " where id.isPrescription = 0 and i.cashierId = {0}", uid).Count();
            return list;
        }

        public bool UpdateStatusInvoiceService(int id, string payMethod)
        {
            var invoice = context.Invoices.SingleOrDefault(s => s.InvoiceId == id);
            if (invoice != null)
            {
                invoice.Status = true;
                invoice.PaymentMethod = payMethod;
                invoice.PaymentDate = DateTime.Now;
                context.Invoices.Update(invoice);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Invoice GetInvoiceDetail (int id)
        {
            if (id <= 0)
            {
                return new Invoice();
            }
            var de = context.Invoices.SingleOrDefault(s=>s.InvoiceId == id);
            return de;
        }

    }
}
