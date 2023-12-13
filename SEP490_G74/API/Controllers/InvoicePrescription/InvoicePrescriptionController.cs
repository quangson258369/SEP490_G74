using HcsBE.Bussiness.InvoicePrescription;
using HcsBE.Bussiness.PrescriptionBusiness;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.InvoicePrescription
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicePrescriptionController:ControllerBase
    {
        [HttpGet("ListPresciptionInfor")]
        public IActionResult GetListPresciptionInfors(int page)
        {
            var logic = new InvoicePrescriptionLogic();
            var output = logic.GetListInvoicePrescription(page);
            return Ok(output);
        }
        [HttpGet("CountListPresciptionInfor")]
        public IActionResult GetCountListPresciptionInfors()
        {
            var logic = new InvoicePrescriptionLogic();
            var output = logic.GetCountListInvoicePrescription();
            return Ok(output);
        }
    }
}
