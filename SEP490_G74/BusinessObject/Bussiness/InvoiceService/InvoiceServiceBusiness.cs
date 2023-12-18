using API.Common;
using AutoMapper;
using DataAccess.Entity;
using HcsBE.Dao.InvoiceServiceDAO;
using HcsBE.Dao.MedicalRecordDAO;
using HcsBE.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.Bussiness.InvoiceService
{
    public class InvoiceServiceBusiness
    {
        private InvoiceServiceDao dao = new InvoiceServiceDao();
        private IMapper mapper;
        public InvoiceServiceBusiness(IMapper _mapper)
        {
            mapper = _mapper;
        }

        public List<InvoiceDTO> GetListInvoiceByCashier(int page, int uid)
        {
            List<Invoice> list = dao.getListInvoiceByCashier(page, uid);
            var output = mapper.Map<List<InvoiceDTO>>(list);
            if (list == null)
            {
                return new List<InvoiceDTO> {
                    new InvoiceDTO()
                    {
                        ExceptionMessage = ConstantHcs.NotFound,
                        ResultCd = ConstantHcs.BussinessError
                    }
                };
            }
            return output;
        }

        public int GetInvoiceServiceCount(int uid)
        {
            return dao.GetInvoiceCount(uid);
        }

        public List<InvoiceDTO> getSearchPagingInvoice(int page, int uid, string str, int status)
        {
            List<Invoice> list = dao.GetFullListInvoice(uid);

            if (list == null)
            {
                return new List<InvoiceDTO> {
                    new InvoiceDTO()
                    {
                        ExceptionMessage = ConstantHcs.NotFound,
                        ResultCd = ConstantHcs.BussinessError
                    }
                };
            }
            else
            {
                int pageSize = 4;
                var output = mapper.Map<List<InvoiceDTO>>(list);
                var filteredList = output
                    .Where(i =>
                    (string.IsNullOrEmpty(str) ||
                    (i.Patient?.Contacts != null &&
                        (i.Patient.Contacts.Name.Contains(str) || i.Patient.Contacts.Phone.Contains(str)))) &&
                        (status < 0 || i.Status == (status != 0))
                    )
                    .OrderByDescending(i => i.InvoiceId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                return filteredList;
            }
        }
    }
}
