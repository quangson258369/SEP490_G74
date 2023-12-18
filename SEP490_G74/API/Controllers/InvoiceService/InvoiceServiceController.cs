using AutoMapper;
using HcsBE.Bussiness.InvoiceService;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.InvoiceService
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceServiceController : ControllerBase
    {
        private IMapper _mapper;
        public InvoiceServiceController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("GetListInvoiceByCashier")]
        public IActionResult GetListInvoiceByCashier(int page, int uid)
        {
            var res = new InvoiceServiceBusiness(_mapper);
            var list = res.GetListInvoiceByCashier(page, uid);
            if (list == null) { return NotFound(); }
            return Ok(list);
        }

        [HttpPost("UpdateStatusInvoiceService")]
        public IActionResult UpdateStatusInvoiceService(int id,string payMethod)
        {
            var res = new InvoiceServiceBusiness(_mapper);
            var status = res.UpdateStatusInvoiceService(id,payMethod);
            return Ok(status);
        }

        [HttpGet("GetInvoiceDetail")]
        public IActionResult GetInvoiceDetail(int id)
        {
            var res = new InvoiceServiceBusiness(_mapper);
            var list = res.GetInvoice(id);
            if (list == null) { return NotFound(); }
            return Ok(list);
        }

        [HttpGet("SearchPagingInvoiceService")]
        public IActionResult SearchPagingInvoiceService(int page, int uid, string str, int status)
        {
            var res = new InvoiceServiceBusiness(_mapper);
            var list = res.getSearchPagingInvoice(page, uid, str, status);
            if (list == null) { return NotFound(); }
            return Ok(list);
        }

        [HttpGet("GetInvoiceServiceCount")]
        public IActionResult GetInvoiceServiceCount(int uid)
        {
            var res = new InvoiceServiceBusiness(_mapper);
            var list = res.GetInvoiceServiceCount(uid);
            return Ok(list);
        }

    }
}
