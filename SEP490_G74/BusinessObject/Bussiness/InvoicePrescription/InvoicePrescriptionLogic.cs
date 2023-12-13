using Azure;
using DataAccess.Entity;
using HcsBE.Dao.InvoicePrescriptionDAO;
using HcsBE.Dao.PrecriptionDAO;
using HcsBE.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.Bussiness.InvoicePrescription
{
    public class InvoicePrescriptionLogic
    {
        private InvoicePrescriptionDAO dao = new InvoicePrescriptionDAO();
        public List<InvoicePrescriptionDTO> GetListInvoicePrescription(int page)
        {
            var status = dao.GetListInvoicePrescription(page);
            return status;
        }
        public int GetCountListInvoicePrescription()
        {
            var count = dao.GetCountListInvoicePrescription();
            return count;
        }
    }
}
