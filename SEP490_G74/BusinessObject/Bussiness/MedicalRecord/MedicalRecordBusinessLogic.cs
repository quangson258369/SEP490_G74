using API.Common;
using AutoMapper;
using HcsBE.Bussiness.Login;
using HcsBE.Dao.MedicalRecordDAO;
using HcsBE.Mapper;
using HcsBE.DTO;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entity;

namespace HcsBE.Bussiness.MedicalRecord
{
    public class MedicalRecordBusinessLogic
    {
        private MedicalRecordDao dao = new MedicalRecordDao();
        private IMapper _mapper;
        public MedicalRecordBusinessLogic(IMapper mapper)
        {
            _mapper = mapper;
        }

        public List<MedicalRecordOutputDto> SearchMedicalRecord(string str, int page)
        {
            List<DataAccess.Entity.MedicalRecord> list = dao.searchMR(str, page);
            var output = _mapper.Map<List<MedicalRecordOutputDto>>(list);
            if (list == null)
            {
                return new List<MedicalRecordOutputDto>()
                {
                    new MedicalRecordOutputDto()
                    {
                        ExceptionMessage = ConstantHcs.NotFound,
                        ResultCd = ConstantHcs.BussinessError
                    }
                };
            }
            return output;
        }

        public bool AddMedicalRecordToInvoice(InvoiceAdd invoice)
        {
            // get list cashier
            List<User> listCashier = dao.GetListCashier();
            // get list uid in invoice
            List<Invoice> listInvoice = dao.GetInvoiceList();
            // lay gia tri convert
            var invoiceConvert = _mapper.Map<Invoice>(invoice);
            // check if have any cashier didn't appear in other Invoice -> add this cashier
            foreach (var ca in listCashier)
            {
                // neu chua co ban ghi thi add vao invoice
                if (!listInvoice.Any(s => s.CashierId == ca.UserId))
                {
                    invoiceConvert.CashierId = ca.UserId;
                    return dao.AddToInvoice(invoiceConvert);
                }
            }

            //get min uid appear in invoice
            var idAppearMin = listInvoice
            .GroupBy(invoice => invoice.CashierId)
            .OrderBy(group => group.Count())
            .First()
            .Key;
            // add vào invoice id xuat hien it nhat
            invoiceConvert.CashierId = idAppearMin;
            bool status = dao.AddToInvoice(invoiceConvert);
            return status;
        }

        public bool AddAutoToInvoiceDetail(InvoiceDetailAdd detail)
        {
            var invd = _mapper.Map<InvoiceDetail>(detail);
            var status = dao.AddToInvoiceDetail(invd);
            return status;
        }

        public List<MedicalRecordOutputDto> GetListMedicalRecord()
        {
            List<DataAccess.Entity.MedicalRecord> list = dao.MedicalRecordList();
            var output = _mapper.Map<List<MedicalRecordOutputDto>>(list);
            if(list == null)
            {
                return new List<MedicalRecordOutputDto>()
                {
                    new MedicalRecordOutputDto()
                    {
                        ExceptionMessage = ConstantHcs.NotFound,
                        ResultCd = ConstantHcs.BussinessError
                    }
                };
            }
            return output;
        }
        public List<MedicalRecordOutputDto> GetListMedicalRecordPaging(int page)
        {
            List<DataAccess.Entity.MedicalRecord> list = dao.MedicalRecordListPaging(page);
            var output = _mapper.Map<List<MedicalRecordOutputDto>>(list);
            if(list == null)
            {
                return new List<MedicalRecordOutputDto>()
                {
                    new MedicalRecordOutputDto()
                    {
                        ExceptionMessage = ConstantHcs.NotFound,
                        ResultCd = ConstantHcs.BussinessError
                    }
                };
            }
            return output;
        }

        public int GetCountOfListMR()
        {
            return dao.GetCountOfListMR();
        }

        public MedicalRecordOutputDto GetMedicalRecord(int id) {
        
            var mr = dao.GetMedicalRecord(id);
            var output = _mapper.Map<MedicalRecordOutputDto>(mr);

            if (mr == null)
            {
                return new MedicalRecordOutputDto()
                {
                    ExceptionMessage = ConstantHcs.NotFound,
                    ResultCd = ConstantHcs.BussinessError
                };
            }
            return output;
        }

        public bool Update( MedicalRecordModify mdto)
        {
            var mr = _mapper.Map<DataAccess.Entity.MedicalRecord>(mdto);
            var status = dao.UpdateMedicalRecord(mr);
            return status;
        }

        public bool AddMR(MedicalRecordModify mdto) 
        {
            var mr = _mapper.Map<DataAccess.Entity.MedicalRecord>(mdto);
            var status = dao.AddMedicalRecord(mr);
            return status;
        }
        
        public string DeleteMR(int id) 
        {
            var mdto = dao.GetMedicalRecord(id);
            if(mdto != null)
            {
                var status = dao.DeleteMedicalRecord(id);
                if(status == "-1")
                {
                    return "-1";
                }
                else
                {
                    return "1";
                }
            }
            return "0";
        }

        public List<DoctorMRDTO> GetDoctorByServiceType(int type)
        {
            var list = dao.GetDoctorByServiceType(type);
            return _mapper.Map<List<DoctorMRDTO>>(list);
        }

        public List<ServiceMRDTO> GetListServiceUses(int id)
        {
            var list = dao.GetServiceUses(id);
            return _mapper.Map<List<ServiceMRDTO>>(list);
        }
    }
}
