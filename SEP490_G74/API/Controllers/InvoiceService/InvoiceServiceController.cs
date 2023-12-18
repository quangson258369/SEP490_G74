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
