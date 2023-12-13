using HcsBE.Bussiness.PrescriptionBusiness;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Entity;
using AutoMapper;
using HcsBE.DTO;
using HcsBE.Bussiness.ServiceBusiness;

namespace API.Controllers.PrescriptionAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController:ControllerBase
    {
        private IMapper mapper;
        public PrescriptionController(IMapper _mapper)
        {
            mapper = _mapper;
        }

        [HttpPost("AddPrescription")]
        public IActionResult AddPrescription(PrescriptionDTO prescription)
        {
            var res = new PrescriptionLogic();
            var rowAffected = res.AddPrescription(prescription);
            return Ok(rowAffected);
        }
        [HttpPost("AddSuppliesInPrescription")]
        public IActionResult AddSuppliesInPrescription(SuppliesPrescriptionDTO prescription)
        {
            var res = new PrescriptionLogic();
            var rowAffected = res.AddSuppliesInPrescription(prescription);
            return Ok(rowAffected);
        }
        [HttpGet("ListPresciptionInfor")]
        public IActionResult GetListPresciptionInfors(int page,int idDoctor)
        {
            var logic = new PrescriptionLogic();
            var output = logic.GetListPresciptionInfors(page, idDoctor);
            return Ok(output);
        }
        [HttpGet("CountListPresciptionInfor")]
        public IActionResult GetCountListPresciptionInfors()
        {
            var logic = new PrescriptionLogic();
            var output = logic.GetCountListPresciptionInfor();
            return Ok(output);
        }
        [HttpGet("ListPresciptionInforsByIdPatient")]
        public IActionResult GetListPresciptionInforsByIdPatient(int page,int idPatient)
        {
            var logic = new PrescriptionLogic();
            var output = logic.GetListPresciptionInforsByIdPatient(page,idPatient);
            return Ok(output);
        }
        [HttpGet("GetPatientContactByPrescriptionId")]
        public IActionResult GetPatientContactByPrescriptionId(int idPrescription)
        {
            var logic = new PrescriptionLogic();
            var output = logic.GetPatientContactByPrescriptionId(idPrescription);
            return Ok(output);
        }
        [HttpGet("GetListSuppliesByPrescriptionId")]
        public IActionResult GetSuppliesByPrescriptionId(int idPrescription)
        {
            var logic = new PrescriptionLogic();
            var output = logic.GetSuppliesByPrescriptionId(idPrescription);
            return Ok(output);
        }
    }
}
