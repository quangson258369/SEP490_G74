using AutoMapper;
using DataAccess.Entity;
using HcsBE.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.Mapper
{
    public class InvoiceMapper: Profile
    {
        public InvoiceMapper() 
        {
            CreateMap<InvoiceAdd, Invoice>();
            CreateMap<InvoiceDetailAdd, InvoiceDetail>();
        
        }
    }
}
